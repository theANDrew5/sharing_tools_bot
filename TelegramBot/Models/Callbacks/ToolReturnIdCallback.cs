//колбек для возврата конкретного инструмента

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
    public class ToolReturnIdCallback : Callback
    {
        public override string Name => "/returntoolid";

        public override string ButtonName => null;//колбек не доступен в команде старт, по этому имени кнопки нет

        public override async Task Execute(CallbackQuery callback, TelegramBotClient client)
        {
            long chatId = callback.From.Id;
            int messageId = callback.Message.MessageId;

            await client.EditMessageTextAsync(chatId, messageId,
                "Пришли фото оборудования, которое хочешь вернуть.",
                replyMarkup: new InlineKeyboardMarkup(new InlineKeyboardButton[] { }));

            int toolId = Int32.Parse(callback.Data.Split(" ")[1]);

            MyUser user = dB.GetUser(chatId);
            Tool tool = dB.GetTool(toolId);

            Task.Run(() => RepliesHandling(chatId, client, user, tool));

        }

        protected override Task RepliesHandling(long chatId, TelegramBotClient client)
        {
            throw new NotImplementedException();
        }

        protected async Task RepliesHandling(long chatId, TelegramBotClient client, MyUser user, Tool tool)
        {
            Message message = await WaitReply(chatId);

            if (message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                if (dB.CloseTransaction(user, tool, message.Photo[2].FileId))
                    await client.SendTextMessageAsync(chatId,
                        "Отлично!\n" +
                        $"Записываем: {user.Name} вернул {tool.Name}"
                        );
            }
            else
                await client.SendTextMessageAsync(chatId,
                    "Неверный формат ответа!\n",
                    replyMarkup: new ReplyKeyboardRemove());
        }
    }
}
