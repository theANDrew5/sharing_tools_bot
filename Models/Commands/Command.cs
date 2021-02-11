﻿//абстрактный класс команд

using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace tel_bot_net.Models.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract Task Execute(Message message, TelegramBotClient client);

        public bool Contains(Message message)
        {
            if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return false;

            return message.Text.Contains(this.Name); //&& message.Text.Contains(AppSettings.Name);
        }
    }
}
