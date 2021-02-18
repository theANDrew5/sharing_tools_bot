using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tel_bot_net.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace tel_bot_net.Models.Commands
{
    public class CleanKeyboardCommand : Command
    {
        public override string Name => "/cleankeyboard";

        public override string Description => "Команда для удаления кастомной клавиатуры";

        public override async Task Execute(Message message, TelegramBotClient client, ReplyHandlerService replyHandler)
        {
            var chatId = message.Chat.Id;

            //ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();

            await client.SendTextMessageAsync(
                chatId: chatId,
                text: "Удаляем клавиатуру",
                replyMarkup: new ReplyKeyboardRemove()
                );
        }
    }
}
