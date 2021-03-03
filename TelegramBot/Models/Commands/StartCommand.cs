//класс команды старт

using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using BotDB.DbModels;


namespace TelegramBot.Models.Commands
{
    public class StartCommand : Command
    {

        public override string Name => "/start";

        public override string Description => "Начать работу";


        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            MyUser user = dB.GetUser(chatId);

            if (user==null)
            {
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
                Task.Run(() => RepliesHandling(chatId, client));
            }
            else
            {
                await client.SendTextMessageAsync(chatId,
                    $"Привет! {user.Name}.\n" +
                    "Вот функции, которыми ты можешь воспользоваться:\n",
                    replyMarkup: Bot.GetFuncKeyboard());
                await client.SendTextMessageAsync(chatId,
                    "Если ты не знаешь как пользоваться функцией введи команду:\n/aboutfunctions\n"
                    );

            }
        }


        protected override async Task RepliesHandling(long chatId, TelegramBotClient client)
        {
            Message message = await WaitReply(chatId);

            if (message.Text == "Отмена")
                await client.SendTextMessageAsync(chatId, "Запрос на регистрацию отклонён.\n" +
                    "Пока ты не зарегистрирован, ты не можешь пользоваться функционалом этого бота.\n" +
                    "Ты всегда можешь зарегистрироваться набрав комманду /start",
                    replyMarkup: new ReplyKeyboardRemove()
                    );
            else if (message.Contact != null)
            {
                MyUser newUser = new MyUser { ChatId = message.Chat.Id, PhoneNumber = message.Contact.PhoneNumber};
                await client.SendTextMessageAsync(chatId, "Отлично! Теперь отправь мне свои фамилию и имя.",
                    replyMarkup: new ReplyKeyboardRemove()
                    );

                message = await WaitReply(chatId);

                if (message.Text != null)
                {
                    newUser.Name = message.Text;
                    if(await dB.AddUser(newUser))
                        await client.SendTextMessageAsync(chatId,
                            $"Привет! {newUser.Name}.\n" +
                            "Вот функции, которыми ты можешь воспользоваться:\n",
                            replyMarkup: Bot.GetFuncKeyboard());
                        await client.SendTextMessageAsync(chatId,
                            "Если ты не знаешь как пользоваться функцией введи команду:\n/aboutfunctions\n"
                            );
                }

            }

        }

    }
}
