//класс команды старт

using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using tel_bot_net.Services;
using tel_bot_net.Models.DbModels;


namespace tel_bot_net.Models.Commands
{
    public class StartCommand : Command
    {

        public override string Name => "/start";

        public override string Description => "Start command";


        public override async Task Execute(Message message, TelegramBotClient client, ReplyHandlerService replyHandler, DataBaseService dbSevice)
        {
            var chatId = message.Chat.Id;

            await client.SendTextMessageAsync(chatId,
                "Привет! Для начала работы тебе необходимо зарегистрироваться.\n" +
                "Мне нужны твои имя, фамилия, и согласие на предоставление твоего телефонного номера.\n" +
                "Если ты согласен нажми ОК, если не согласен нажми Отмена.\n",
                replyMarkup: new ReplyKeyboardMarkup(new KeyboardButton[]
                {
                new KeyboardButton{ Text = "Ok", RequestContact = true},
                new KeyboardButton{ Text = "Отмена"}
                }, oneTimeKeyboard: true)
                );

            Task.Run(() => RepliesHandling(chatId, client, replyHandler, dbSevice));
        }


        protected override async Task RepliesHandling(long chatId, TelegramBotClient client, ReplyHandlerService replyHandler, DataBaseService dbService)
        {
            Message message = await WaitReply(chatId, replyHandler);

            if (message.Text == "Отмена")
                await client.SendTextMessageAsync(chatId, "Запрос на регистрацию отклонён.\n" +
                    "Пока вы не зарегистрированы, вы не можете пользоваться функционалом этого бота.\n" +
                    "Вы всегда можете зарегистрироваться набрав комманду /start",
                    replyMarkup: new ReplyKeyboardRemove()
                    );
            else if (message.Contact != null)
            {
                MyUser newUser = new MyUser { Id = message.Chat.Id, PhoneNumber = message.Contact.PhoneNumber};
                await client.SendTextMessageAsync(chatId, "Отлично! Теперь отправь мне свои фамилию и имя.",
                    replyMarkup: new ReplyKeyboardRemove()
                    );

                message = await WaitReply(chatId, replyHandler);

                if (message.Text != null)
                {
                    newUser.Name = message.Text;
                    if(await dbService.AddUser(newUser))
                        await client.SendTextMessageAsync(chatId, "Отлично! Теперь ты зарегистрирован и можешь пользоваться функционалом этого бота.");
                }

            }

        }

    }
}
