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
        private UpdateHolderService _updateHolder = Program.UpdateHolder;

        public override string Name => "/testreplies";

        public override string Description => "Test handling user's replies.";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            Thread T = Thread.CurrentThread;

            await client.SendTextMessageAsync(chatId, "Waiting yuour answer!");
            Task.Run(() => WaitReply(chatId, client));
        }

        private async Task WaitReply(long chatId, TelegramBotClient client)
        {
            _updateHolder.VaitReply(chatId);

            Task<Update> replyUpdate = Task.Run(() => _updateHolder.DeHold(chatId));
            replyUpdate.Wait();

            Message replyMessage = replyUpdate.Result.Message;

            await client.SendTextMessageAsync(chatId, $"Your answer was: {replyMessage.Text}");
        }

    }
}
