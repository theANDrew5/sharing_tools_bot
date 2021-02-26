using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Models.Commands
{
    public class CleanKeyboardCommand : Command
    {
        public override string Name => "/cleankeyboard";

        public override string Description => "Команда для удаления кастомной клавиатуры";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            //ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Удаляем клавиатуру",
                replyMarkup: new ReplyKeyboardRemove()
                );
        }

        protected override Task RepliesHandling(long chatId, TelegramBotClient client)
        {
            throw new NotImplementedException();
        }
    }
}
