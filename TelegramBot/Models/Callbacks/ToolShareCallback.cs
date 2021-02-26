//клас описывает колбек для раздачи оборудования

using System.Threading.Tasks;
using tel_bot_net.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace tel_bot_net.Models.Callbacks
{
    public class ToolShareCallback : Callback
    {
        public override string Name => "toolshare";

        public override string ButtonName => "Взять инструмент";

        public override async Task Execute(CallbackQuery callback, TelegramBotClient client, DataBaseService dbService)
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
            Task.Run(() => RepliesHandling(chatId, client, dbService));
        }

        protected override async Task RepliesHandling(long chatId, TelegramBotClient client, DataBaseService dbService)
        {
            Message message = await WaitReply(chatId);

            switch (message.Text)
            {
                case "Я не знаю правила":
                    await client.SendTextMessageAsync(chatId,
                        "Здесь надо будет написать правила.\n",
                        replyMarkup: new ReplyKeyboardMarkup(new KeyboardButton[][]
                        {
                            new KeyboardButton [] { new KeyboardButton ("Продолжить")},
                            new KeyboardButton [] { new KeyboardButton ("Отмена")}
                        }, oneTimeKeyboard: true)
                        );
                    break;

                case "Я согласен с правилами, продолжить":
                    await client.SendTextMessageAsync(chatId,
                        "OK, не говори потом, что не предупреждали.\n" +
                        "Введи ID оборудования.",
                        replyMarkup: new ReplyKeyboardRemove());

                    message = await WaitReply(chatId);

                    await client.SendTextMessageAsync(chatId,
                        $"ID = {message.Text}\n",
                        replyMarkup: new ReplyKeyboardRemove());

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
