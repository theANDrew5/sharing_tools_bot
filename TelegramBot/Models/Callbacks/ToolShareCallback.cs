//клас описывает колбек для раздачи оборудования

using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using BotDB.DbModels;

namespace TelegramBot.Models.Callbacks
{
    public class ToolShareCallback : Callback
    {
        public override string Name => "toolshare";

        public override string ButtonName => "Взять инструмент";

        public override async Task Execute(CallbackQuery callback, TelegramBotClient client)
        {
            var chatId = callback.From.Id;

            
            await client.SendTextMessageAsync(chatId,
                "Воспользовавшись функцией выдачи оборудования, ты соглашаешься с правилами пользования сервисом",
                replyMarkup: new ReplyKeyboardMarkup( new KeyboardButton [] []
                {
                    new KeyboardButton [] { new KeyboardButton ("Я не знаю правила")},
                    new KeyboardButton [] { new KeyboardButton ("Я согласен с правилами, продолжить")},
                    new KeyboardButton [] { new KeyboardButton ("Отмена")}
                }, oneTimeKeyboard: true)
                );
            Task.Run(() => RepliesHandling(chatId, client));
        }

        protected override async Task RepliesHandling(long chatId, TelegramBotClient client)
        {
            Message message = await WaitReply(chatId);

            switch (message.Text)
            {
                case "Я не знаю правила":
                    await client.SendTextMessageAsync(chatId,
                        "Здесь надо будет написать правила.\n",
                        replyMarkup: new ReplyKeyboardMarkup(new KeyboardButton[]
                        {
                            new KeyboardButton { Text = "Продолжить" },
                            new KeyboardButton { Text = "Отмена" }
                        }, oneTimeKeyboard: true)
                        );
                    message = await WaitReply(chatId);

                    switch(message.Text)
                    {
                        case "Продолжить":
                            GetToolsShare(chatId, client);
                            break;

                        case "Отмена":
                            await client.SendTextMessageAsync(chatId,
                                "OK, отмена.\n",
                                replyMarkup: new ReplyKeyboardRemove());
                            break;
                    }

                    break;


                case "Я согласен с правилами, продолжить":
                    GetToolsShare(chatId, client);
                    break;

                case "Отмена":
                    await client.SendTextMessageAsync(chatId,
                        "OK, отмена.\n",
                        replyMarkup: new ReplyKeyboardRemove());
                    break;

            }
                        

        }

        private async void GetToolsShare (long chatId, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(chatId,
                "OK, не говори потом, что не предупреждали.\n" +
                "Введи ID оборудования.",
                replyMarkup: new ReplyKeyboardRemove());

            Message message = await WaitReply(chatId);
            int toolId = Int32.Parse(message.Text);

            MyUser user = dBMethods.GetUser(chatId);
            Tool tool = dBMethods.GetTool(toolId);

            if (tool==null)
                await client.SendTextMessageAsync(chatId,
                    "Не могу найти такой инструмент.\n" +
                    "Попробуй ещё раз или обратись к администратору.",
                    replyMarkup: new ReplyKeyboardRemove());
            else
            {
                await client.SendTextMessageAsync(chatId,
                    "Проверь данные.\n"+
                    $"Наименование: {tool.Name}\n"+
                    $"Инвентарный номер: {tool.InventoryNumber}",
                    replyMarkup: new ReplyKeyboardMarkup(new KeyboardButton[]
                    {
                        new KeyboardButton { Text = "Продолжить" },
                        new KeyboardButton { Text = "Отмена" }
                    }, oneTimeKeyboard: true)
                    );
                message = await WaitReply(chatId);

                switch (message.Text)
                {
                    case "Продолжить":
                        Transaction transaction = new Transaction { User = user, Tool = tool, DateTimeOpen = DateTime.Now };
                        if (dBMethods.AddTransaction(transaction))
                        {
                            await client.SendTextMessageAsync(chatId,
                                $"Отлично!. Записали {user.Name} взял {tool.Name}.\n" +
                                "Не забудь вернуть!",
                                replyMarkup: new ReplyKeyboardRemove());
                        }
                        break;

                    case "Отмена":
                        await client.SendTextMessageAsync(chatId,
                            "OK, отмена.\n",
                            replyMarkup: new ReplyKeyboardRemove());
                        break;
                }

            }

        }
    }
}
