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

        public override string Description => "Это функция с помощью которой ты можешь взять оборудование и инструменты из аудитории.\n" +
            "Тебе необходимо проделать следующие действия:\n" +
            "1. Найти идентификатор оборудования (наклейка \x22ID=X\x22).\n" +
            "2. Активировать функцию через команду /start и кнопку \x22Взять инструмент\x22.\n" +
            "3. Ввести идентификатор оборудования и проверить данные оборудования, выданные системой.\n" +
            "4. Отправить боту фотографию оборудования.\n" +
            "После этого в базе данных системы сохранится информация о том, что и когда ты взял.\n";

        private string rules = "Правила:\n" +
            "1. Ты несёшь ответственность за оборудование, которое взял.\n" +
            "2. Ты обязан вернуть оборудование в той комплектации, в которой ты его взял.\n" +
            "3. Я прошу тебя предоставить фото, для того чтобы убедиться в сохранности оборудования, пока ты им пользовался." +
            "Если ты предоставил фото не надлежащего качества или не отражающее вид оборудования, которое ты берёшь. " +
            "Я в праве считать, что дефекты обнаруженные на оборудовании после твоего использования нанёс именно ты.\n" +
            "4. Если ты берёшь оборудование на срок более одного рабочего дня, сообщи ответственному за аудиторию!\n" +
            "Эти правила не несут в себе цели кого либо запугать и направлены только на сохранность оборудования." +
            "Если в процессе эксплуатации ты обнаружил какие либо дефекты или же сам их нанёс, обратись к ответсвенному за аудиторию," +
            "он обязательно разберётся в твоей проблеме";

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
                        rules,
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

            MyUser user = dB.GetUser(chatId);
            Tool tool = dB.GetTool(toolId);

            if (tool==null)
                await client.SendTextMessageAsync(chatId,
                    "Не могу найти такой инструмент.\n" +
                    "Попробуй ещё раз или обратись к администратору.",
                    replyMarkup: new ReplyKeyboardRemove());
            else
            {
                if (dB.GetOpenTransaction(tool.Id) == null)
                {
                    await client.SendTextMessageAsync(chatId,
                        "Проверь данные.\n" +
                        $"Наименование: {tool.Name}\n" +
                        $"Инвентарный номер: {tool.InventoryNumber}",
                        replyMarkup: new ReplyKeyboardMarkup(new KeyboardButton[]
                        {
                                            new KeyboardButton { Text = "Продолжить" },
                                            new KeyboardButton { Text = "Отмена" }
                        }, oneTimeKeyboard: true)
                        );
                    message = await WaitReply(chatId);
                }
                else
                {
                    await client.SendTextMessageAsync(chatId,
                        "Это оборудование сейчас выдано.\n" +
                        "Если это не так обратись к администратору."
                        );
                    return;
                }


                switch (message.Text)
                {
                    case "Продолжить":
                        await client.SendTextMessageAsync(chatId,
                            "ОК, теперь отправь мне фотографию оборудования.\n",
                            replyMarkup: new ReplyKeyboardRemove());

                        message = await WaitReply(chatId);

                        if (message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
                        {
                            if (dB.OpenTransaction(user, tool, message.Photo[2].FileId))
                            {
                                await client.SendTextMessageAsync(chatId,
                                    $"Отлично!. Записали {user.Name} взял {tool.Name}.\n" +
                                    "Не забудь вернуть!",
                                    replyMarkup: new ReplyKeyboardRemove());
                            }
                        }
                        else
                            await client.SendTextMessageAsync(chatId,
                                "Неверный формат ответа!\n",
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
}
