
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using BotDB.DbModels;

namespace TelegramBot.Models.Commands
{
    public class AboutFunctionsCommand : Command
    {
        public override string Name => "/aboutfunctions";

        public override string Description => "Как работают функции";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            MyUser user = dB.GetUser(chatId);

            if (user == null)
            {
                await client.SendTextMessageAsync(chatId,
                    "Привет! Для начала работы тебе необходимо зарегистрироваться.\n" +
                    "Для регистрации воспользуйся коммандой:\n/start\n"
                    );
            }
            else
            {
                var functions = Bot.GetFunctions();

                await client.SendTextMessageAsync(chatId,
                    $"Привет! {user.Name}.\n"
                    );

                foreach (var function in functions)
                {
                    await client.SendTextMessageAsync(chatId,
                        text: function.Description,
                        replyMarkup: new InlineKeyboardMarkup(
                            new InlineKeyboardButton[] {
                                InlineKeyboardButton.WithCallbackData(function.ButtonName,function.Name)
                            })) ;
                }

            }
        }

        protected override Task RepliesHandling(long chatId, TelegramBotClient client)
        {
            throw new NotImplementedException();
        }
    }

}
