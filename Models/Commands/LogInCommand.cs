using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using tel_bot_net.Models.DbModels;

namespace tel_bot_net.Models.Commands
{
    public class LogInCommand : Command
    {

        public override string Name => "/login";

        public override string Description => "Login command";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            KeyboardButton[] keyboard = new KeyboardButton[]
            {
                ("Ввести данные"),
                ("Отмена")
            };

            ReplyKeyboardMarkup keyboardMarkup = new ReplyKeyboardMarkup(keyboard, oneTimeKeyboard:true);

            await client.SendTextMessageAsync(chatId, "Введите имя и фамилию", replyMarkup:keyboardMarkup);
            

        }
    }
}
