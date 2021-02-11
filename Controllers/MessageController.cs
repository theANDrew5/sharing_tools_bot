//контроллер сообщений

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tel_bot_net.Models;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;


namespace tel_bot_net.Controllers
{
    [Route("api/message/update")]
    [ApiController]
    public class MessageController : Controller
    {
        [HttpGet]
        public string OnGet()
        {
            return "Method GET unuvalable";
        }

        [HttpPost]
        public async Task<OkResult> update([FromBody]Update update)
        {
            //if (update == null) return Ok();

            var commands = Bot.Commands;
            var message = update.Message;
            var botClient = await Bot.GetBotClientAsync();

            foreach (var command in commands)
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, botClient);
                    break;
                }
            }

            return Ok();
        }
    }
}
