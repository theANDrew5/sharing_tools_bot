//команда для тестов

using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System;
using System.Threading;
using System.Collections.Generic;

namespace tel_bot_net.Models.Commands
{
    public class TestCommand : Command
    {
        public override string Name => "/test";

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
            

#if DEBUG
            Thread T = Thread.CurrentThread;

            Console.WriteLine($"Имя потока: {T.Name}");
            Console.WriteLine($"Ссылка на поток: {T}");
#endif

            ReplyKeyboardRemove removeKeyboard = new ReplyKeyboardRemove();
/*
            if (replyMessage.Text.Contains("1"))
            {
                await client.SendTextMessageAsync(chatId, "Ты ввёл 1",replyMarkup : removeKeyboard);
            }
            else if (replyMessage.Text.Contains("2"))
            {
                await client.SendTextMessageAsync(chatId, "Ты ввёл два");
            }
*/

        }
    }
}
