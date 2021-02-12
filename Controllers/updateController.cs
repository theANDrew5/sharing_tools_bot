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
    [Route("api/update")]
    [ApiController]
    public class updateController : Controller
    {
        [HttpGet]
        public IActionResult OnGet(string type, int id)
        {
            return Redirect("https://t.me/ToShar_bot");
        }

        [HttpPost]
        public async Task<OkResult> update([FromBody] Update update)
        {

            if (update.Message == null) return Ok();

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
