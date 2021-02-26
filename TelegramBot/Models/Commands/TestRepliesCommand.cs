using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Services;

namespace TelegramBot.Models.Commands
{
    public class TestRepliesCommand : Command
    {
        public override string Name => "/testreplies";

        public override string Description => "Test handling user's replies.";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            Thread T = Thread.CurrentThread;

            await client.SendTextMessageAsync(chatId, "Waiting yuour answer!");
            Task.Run(() => RepliesHandling(chatId, client));
        }

        protected override async Task RepliesHandling(long chatId, TelegramBotClient client)
        {
            Message replyMessage = await WaitReply(chatId);

            await client.SendTextMessageAsync(chatId, $"Your answer was: {replyMessage.Text}");
        }
    }
}
