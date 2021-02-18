using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using tel_bot_net.Services;

//[ApiController]

namespace tel_bot_net.Controllers
{
    [Route("api/update")]
    [ApiController]
    public class ApiController : Controller
    {
        //внедрение зависимости от сериса
        private readonly MessageHandlerService _messageHandler;
        private readonly CallbackHandlerService _callbackHandler;

        public ApiController(MessageHandlerService messageHandler, CallbackHandlerService callbackHandler)
        {
            _messageHandler = messageHandler;
            _callbackHandler = callbackHandler;
        }
        //

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
                if (await _messageHandler.Handle(update))
                    return Ok();
            }

            if (update.Type == UpdateType.CallbackQuery)
            {
                if (await _callbackHandler.Handle(update))
                    return Ok();
            }
            return Ok();
        }
  
    }
}
