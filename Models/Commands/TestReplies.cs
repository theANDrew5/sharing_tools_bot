using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using tel_bot_net.Services;

namespace tel_bot_net.Models.Commands
{
    public class TestReplies : Command
    {
        public override string Name => "/testreplies";

        public override string Description => "Test handling user's replies.";

        public override async Task Execute(Message message, TelegramBotClient client, ReplyHandlerService replyHandler, DataBaseService dbService)
        {
            var chatId = message.Chat.Id;
            Thread T = Thread.CurrentThread;

            await client.SendTextMessageAsync(chatId, "Waiting yuour answer!");
            Task.Run(() => RepliesHandling(chatId, client,replyHandler, dbService));
        }

        protected override async Task RepliesHandling(long chatId, TelegramBotClient client, ReplyHandlerService replyHandler, DataBaseService dbService)
        {
            Message replyMessage = await WaitReply(chatId, replyHandler);

            await client.SendTextMessageAsync(chatId, $"Your answer was: {replyMessage.Text}");
        }
    }
}
