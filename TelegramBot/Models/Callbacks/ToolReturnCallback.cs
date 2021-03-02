//класс описывает колбек для возврата оборудования

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using BotDB.DbModels;

namespace TelegramBot.Models.Callbacks
{
    public class ToolReturnCallback : Callback
    {
        public override string Name => "toolreturn";

        public override string ButtonName => "Вернуть инструмент";

        public override async Task Execute(CallbackQuery callback, TelegramBotClient client)
        {
            long chatId = callback.From.Id;
            List<Transaction> transactions = dB.GetOpenTransactions(chatId);

            List<InlineKeyboardButton[]> keyboard = new List<InlineKeyboardButton[]>();

            foreach (var tr in transactions)
            {
                keyboard.Add(new InlineKeyboardButton[]
                    { InlineKeyboardButton.WithCallbackData($"{tr.Tool.Name}\tID={tr.Tool.Id}", $"/returntoolid {tr.Tool.Id}")});
            }

            await client.SendTextMessageAsync(chatId,
                "Выбери инструмент, который хочешь вернуть",
                replyMarkup: new InlineKeyboardMarkup(keyboard));
        }

        protected override Task RepliesHandling(long chatId, TelegramBotClient client)
        {
            throw new NotImplementedException();
        }
    }
}
