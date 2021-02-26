//команда для тестов


using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TelegramBot.Models.Commands
{
    public class TestInlineButtonsCommand : Command
    {
        public override string Name => "/testinline";

        public override string Description => "Test command";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            List<InlineKeyboardButton[]> keyboard = new List<InlineKeyboardButton[]>();

            keyboard.Add(new[] { InlineKeyboardButton.WithCallbackData("1", "/test 1") });
            keyboard.Add(new[] { InlineKeyboardButton.WithCallbackData("2", "/test 2") });

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Тест",
                replyMarkup: new InlineKeyboardMarkup(keyboard)
                );
        }

        protected override Task RepliesHandling(long chatId, TelegramBotClient client)
        {
            throw new NotImplementedException();
        }
    }
}
