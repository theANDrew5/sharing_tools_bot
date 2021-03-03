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

        public override string Description => "Это функция с помощью которой ты можешь вернуть оборудование и инструменты, которые ты взял.\n" +
            "Тебе необходимо проделать следующие действия:\n" +
            "1. Найти идентификатор оборудования (наклейка \x22ID=X\x22).\n" +
            "2. Активировать функцию через команду /start и кнопку \x22Вернуть инструмент\x22.\n" +
            "3. Ввести идентификатор оборудования и проверить данные оборудования, выданные системой.\n" +
            "4. Отправить боту фотографию оборудования.\n" +
            "После этого в базе данных системы сохранится информация о том, что ты вернул оборудование.\n";

        public override async Task Execute(CallbackQuery callback, TelegramBotClient client)
        {
            long chatId = callback.From.Id;
            List<Transaction> transactions = dB.GetOpenTransactions(chatId);

            List<InlineKeyboardButton[]> keyboard = new List<InlineKeyboardButton[]>();

            if (transactions.Count == 0)
            {
                await client.SendTextMessageAsync(chatId,
                    "Ты уже вернул все инструменты. Молодец!"
                    );
                return;
            }

            foreach (var tr in transactions)
            {
                keyboard.Add(new InlineKeyboardButton[]
                    { InlineKeyboardButton.WithCallbackData($"{tr.Tool.Name}\tID={tr.Tool.Id}", $"/returntoolid {tr.Tool.Id}")});
            }

            keyboard.Add(new InlineKeyboardButton[]
                { InlineKeyboardButton.WithCallbackData("Отмена", $"/returntoolid cancel")});

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
