using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace tel_bot_net.Controllers
{

    public class CallbackController : Controller
    {
        //[HttpGet]
        //[Route("callback/update")]
        //public IActionResult OnGet(string type, int id)
        //{
        //    return Redirect("https://t.me/ToShar_bot");
        //}
        public async Task<OkResult> CalbackHandling(Update update)
        {
#if DEBUG
            Console.WriteLine("Calback Query handled");
#endif
            return Ok();
        }
    }
}
