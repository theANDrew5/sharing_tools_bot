using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using tel_bot_net.Controllers;

//[ApiController]

namespace tel_bot_net.Controllers
{
    [Route("api/update")]
    [ApiController]
    public class ApiController : Controller
    {

        [HttpGet]
        public IActionResult OnGet(string type, int id)
        {
            return Redirect("https://t.me/ToShar_bot");
        }


        [HttpPost]
        public async Task<OkResult> update([FromBody] Update update)
        {
            if(update.Type == UpdateType.Message)
            {
                //return Ok();
                MessageController controller = new MessageController();
                return await controller.MessageHandling(update);
            }

            if (update.Type == UpdateType.CallbackQuery)
            {
                //return Ok();
                CallbackController controller = new CallbackController();
                return await controller.CalbackHandling(update);
            }
            return Ok();
        }
  
    }
}
