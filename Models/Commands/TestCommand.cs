
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace tel_bot_net.Models.Commands
{
    public class TestCommand : Command
    {
        public override string Name => "/test";

        public override string Description => "Test command";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            if (message.Text.Contains("?id="))
            {
                var point = message.Text.LastIndexOf("=");
                string Id = message.Text.Substring(point+1);

                await client.SendTextMessageAsync(chatId, $"Succes! Your id = {Id}.");
            }
            else
            {
                await client.SendTextMessageAsync(chatId, "Don't have id.");
            }
        }
    }
}
