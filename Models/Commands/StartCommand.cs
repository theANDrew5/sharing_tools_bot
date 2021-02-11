//класс команды старт

using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace tel_bot_net.Models.Commands
{
    public class StartCommand : Command
    {
        public override string Name => "/start";
        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            await client.SendTextMessageAsync(chatId, "It's done. You are grait!");

        }

    }
}
