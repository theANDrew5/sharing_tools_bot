//Абстрактный класс calback кнопок

using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using tel_bot_net.Services;

namespace tel_bot_net.Models.Callbacks
{
    public abstract class Callback
    {
        public abstract string Name { get; }// имя колбека и иего дата

        public abstract string ButtonName { get; } // текст на кнопке

        public abstract Task Execute(CallbackQuery callback, TelegramBotClient client, DataBaseService dbService);

        protected abstract Task RepliesHandling(long chatId, TelegramBotClient client, DataBaseService dbService);

        protected async Task<Message> WaitReply(long chatId)
        {
            ReplyHandler.WaitReply(chatId);
            Update replyUpdate = await ReplyHandler.DeHoldAsync(chatId);
            return replyUpdate.Message;
        }

        public bool Contains(string data)
        {

            return data.Contains(this.Name);
        }

        public InlineKeyboardButton GetKey()
        {
            return InlineKeyboardButton.WithCallbackData(ButtonName, Name);
        }
    }
}
