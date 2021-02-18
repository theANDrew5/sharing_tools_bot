using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using tel_bot_net.Services;

namespace tel_bot_net.Models.Commands
{
    public class LogInCommand : Command
    {

        public override string Name => "/login";

        public override string Description => "Login command";

        public override async Task Execute(Message message, TelegramBotClient client, ReplyHandlerService replyHandler)
        {
            var chatId = message.Chat.Id;

            KeyboardButton permiss = new KeyboardButton("ОК");
            permiss.RequestContact = true;

            KeyboardButton cancel = new KeyboardButton("Отмена");

            KeyboardButton[] keyboard =  
            {
                permiss,
                cancel
            };

            ReplyKeyboardMarkup keyboardMarkup = new ReplyKeyboardMarkup(keyboard, oneTimeKeyboard:true);

            if (message.Contact == null)
            {
                await client.SendTextMessageAsync(chatId, "Для регистрации необходимо получить ваш номер телефона.\n" +
                    "Для подтвеждения регистраци нажмите: ОК. \n" +
                    "Если вы отказываетесь предоставить номер нажмите: Отмена ", replyMarkup: keyboardMarkup);
            }

        }
    }
}
