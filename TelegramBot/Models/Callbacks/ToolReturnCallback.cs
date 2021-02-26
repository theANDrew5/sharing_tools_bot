using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Models.Callbacks
{
    public class ToolReturnCallback : Callback
    {
        public override string Name => "toolreturn";

        public override string ButtonName => "Вернуть инструмент";

        public override Task Execute(CallbackQuery callback, TelegramBotClient client)
        {
            throw new NotImplementedException();
        }

        protected override Task RepliesHandling(long chatId, TelegramBotClient client)
        {
            throw new NotImplementedException();
        }
    }
}
