﻿//описание бота
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using tel_bot_net.Models.Commands;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace tel_bot_net.Models
{
    public static class Bot
    {
        private static TelegramBotClient botClient;
        private static List<Commands.Command> commandList = new List<Commands.Command>();
        public static IReadOnlyList<Commands.Command> Commands => commandList.AsReadOnly();//список команд
        private static List<BotCommand> BotCommandList = new List<BotCommand>();

        public static async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (botClient != null)
            {
                return botClient;
            }

            //комманды добавлять сдесь
            commandList.Add(new StartCommand());
            commandList.Add(new LogInCommand());
            commandList.Add(new TestCommand());

            //заполняем список команд для API
            foreach (var command in Commands)
            {
                var temp = new BotCommand();
                temp.Command = command.Name.Substring(1);
                temp.Description = command.Description;
                BotCommandList.Add(temp);
            }

            //создание бота
            botClient = new TelegramBotClient(AppSettings.Key);

            //настройка хука
            string hook = string.Format(AppSettings.Url, "/api/update");//ссылка на тунель Ngrock + ссылка на путь контроллера
            //string hook2 = string.Format(AppSettings.Url,)
            await botClient.SetWebhookAsync(hook);

            //Настройка команд для API
            await botClient.SetMyCommandsAsync(BotCommandList); 

            return botClient;

        }


    }
}
