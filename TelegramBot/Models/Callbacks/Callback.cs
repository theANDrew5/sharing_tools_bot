﻿//Абстрактный класс calback кнопок

using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Singletones;
using BotDB;

namespace TelegramBot.Models.Callbacks
{
    public abstract class Callback
    {
        private ReplyHandler replyHandler = ReplyHandler.GetInstance();
        protected DBMethods dB = DBMethods.GetInstance();

        public abstract string Name { get; }// имя колбека и иего дата

        public abstract string ButtonName { get; } // текст на кнопке

        public abstract string Description { get; }

        public abstract Task Execute(CallbackQuery callback, TelegramBotClient client);

        protected abstract Task RepliesHandling(long chatId, TelegramBotClient client);

        protected async Task<Message> WaitReply(long chatId)
        {
            replyHandler.WaitReply(chatId);
            Update replyUpdate = await replyHandler.DeHoldAsync(chatId);
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
