using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Models.Commands
{
    public class TestPhotoCommand : Command
    {
        public override string Name => "/testphoto";

        public override string Description => "всё, что попадает в телеграм, остаётся в телеграмме";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            long chatId = message.Chat.Id;

            await client.SendTextMessageAsync(chatId,
                "Ты его точно удалил?"                
                );

            await client.SendPhotoAsync(chatId,
                "AgACAgIAAxkBAAIEwmA-D-9iT4RpriC0VROVOIbBV-8iAALesDEbaAzxST8NvLj8j4QZo2oAAZ8uAAMBAAMCAAN5AAN1ugACHgQ"
                );
        }

        protected override Task RepliesHandling(long chatId, TelegramBotClient client)
        {
            throw new NotImplementedException();
        }
    }
}
