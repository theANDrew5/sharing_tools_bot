
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace tel_bot_net.Models.Commands
{
    public class TestCommand : Command
    {
        public override string Name => "/test";

        public override string Description => "Test command";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            KeyboardButton one = new KeyboardButton("1");

            KeyboardButton two = new KeyboardButton("2");

            KeyboardButton[] keyboard =
            {
                one,
                two
            };

            ReplyKeyboardMarkup keyboardMarkup = new ReplyKeyboardMarkup(keyboard, oneTimeKeyboard: true);

            await client.SendTextMessageAsync(chatId, "Тест", replyMarkup: keyboardMarkup);

            if (message.Text.Contains("1"))
            {
                await client.SendTextMessageAsync(chatId, "Ты ввёл 1");
            }
            else if (message.Text.Contains("2"))
            {
                await client.SendTextMessageAsync(chatId, "Ты ввёл два");
            }
        }
    }
}
