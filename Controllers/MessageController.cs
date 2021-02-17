//контроллер сообщений

using Microsoft.AspNetCore.Mvc;
using tel_bot_net.Models;
using Telegram.Bot.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace tel_bot_net.Controllers
{
    public class MessageController : Controller
    {

        //public IActionResult OnGet(string type, int id)
        //{
        //    return Redirect("https://t.me/ToShar_bot");
        //}
        public async Task<OkResult> MessageHandling(Update update)
        {

#if DEBUG
            Console.WriteLine("Message Query handled");
#endif

            if (update.Message == null) return Ok();

            var commands = Bot.Commands;
            var message = update.Message;
            var botClient = await Bot.GetBotClientAsync();

            foreach (var command in commands)
            {
                if (command.Contains(message))
                {
#if DEBUG
                    Console.WriteLine($"Start execute commant: {command.Name}");
#endif

                    await command.Execute(message, botClient);
#if DEBUG
                    Console.WriteLine($"Stop execute commant: {command.Name}");
#endif
                    break;
                }
            }

            return Ok();
        }

    }
}
