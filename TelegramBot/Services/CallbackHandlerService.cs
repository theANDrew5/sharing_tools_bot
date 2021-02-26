﻿using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using tel_bot_net.Models;

namespace tel_bot_net.Services
{
    public class CallbackHandlerService
    {

        public async Task<bool> Handle(Update update)
        {
#if DEBUG
            Console.WriteLine("Calback query handled");
#endif

            //if (replyHandler.Hold(update))
            //    return true;

            var callbacks = Bot.callbacks;
            var calbackQuery = update.CallbackQuery;
            var botClient = await Bot.GetBotClientAsync();


            foreach (var callback in callbacks)
            {
                if (callback.Contains(calbackQuery.Data))
                {
#if DEBUG
                    Console.WriteLine($"Start execute commant: {callback.Name}");
#endif
                    await callback.Execute(calbackQuery, botClient);
#if DEBUG
                    Console.WriteLine($"Stop execute commant: {callback.Name}");
#endif
                    break;
                }
            }

            return true;
        }
        
    }
}
