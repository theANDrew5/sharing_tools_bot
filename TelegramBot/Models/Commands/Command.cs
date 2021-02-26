//абстрактный класс команд

using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using tel_bot_net.Singletones;

namespace tel_bot_net.Models.Commands
{
    public abstract class Command
    {

        private ReplyHandler replyHandler = ReplyHandler.GetInstance();
        protected DBMethods dBMethods = DBMethods.GetInstance();
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract Task Execute(Message message, TelegramBotClient client);

        protected abstract Task RepliesHandling(long chatId, TelegramBotClient client);

        protected async Task<Message> WaitReply(long chatId)
        {
            replyHandler.WaitReply(chatId);
            Update replyUpdate = await replyHandler.DeHoldAsync(chatId);
            return replyUpdate.Message;
        }


        public bool Contains(Message message)
        {
            if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return false;

            return message.Text.Contains(this.Name);
        }

    }
}
