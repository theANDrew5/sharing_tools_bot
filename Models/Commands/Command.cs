//абстрактный класс команд

using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using tel_bot_net.Services;

namespace tel_bot_net.Models.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract Task Execute(Message message, TelegramBotClient client, ReplyHandlerService replyHandler, DataBaseService dbService);

        protected abstract Task RepliesHandling(long chatId, TelegramBotClient client, ReplyHandlerService replyHandler, DataBaseService dbService);

        protected async Task<Message> WaitReply(long chatId, ReplyHandlerService replyHandler)
        {
            replyHandler.WaitReply(chatId);

            Task<Update> replyUpdate = Task.Run(() => replyHandler.DeHold(chatId));
            replyUpdate.Wait();

            return replyUpdate.Result.Message;
        }


        public bool Contains(Message message)
        {
            if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return false;

            return message.Text.Contains(this.Name); //&& message.Text.Contains(AppSettings.Name);
        }

    }
}
