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

            KeyboardButton one = new KeyboardButton("1");

            KeyboardButton two = new KeyboardButton("2");

            KeyboardButton[] keyboard =
            {
                one,
                two
            };

            ReplyKeyboardMarkup keyboardMarkup = new ReplyKeyboardMarkup(keyboard, oneTimeKeyboard: true);

            await client.SendTextMessageAsync(chatId, "Тест", replyMarkup: keyboardMarkup);
            Program.ReplyChatIds.Add(chatId);//вносим чат id в лист ожидания

            Message replyMessage = null;

            while (true)
            {
                if (!Program.ReplyMessages.TryPeek(out replyMessage))
                    Thread.Sleep(100);
                else if (replyMessage.Chat.Id != chatId)
                    Thread.Sleep(100);
                else
                    break;
            }

            replyMessage = Program.ReplyMessages.Dequeue();//забираем сообщение из очереди
            Program.ReplyChatIds.Remove(chatId);// удаляем чат id из списка ожидания

#if DEBUG
            Thread T = Thread.CurrentThread;

            Console.WriteLine($"Имя потока: {T.Name}");
            Console.WriteLine($"Ссылка на поток: {T}");
#endif

            ReplyKeyboardRemove removeKeyboard = new ReplyKeyboardRemove();

            if (replyMessage.Text.Contains("1"))
            {
                await client.SendTextMessageAsync(chatId, "Ты ввёл 1",replyMarkup : removeKeyboard);
            }
            else if (replyMessage.Text.Contains("2"))
            {
                await client.SendTextMessageAsync(chatId, "Ты ввёл два");
            }


        }
    }
}
