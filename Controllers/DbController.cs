using Microsoft.AspNetCore.Mvc;
using tel_bot_net.Models;
using tel_bot_net.Models.DbModels;
using System.Threading.Tasks;

namespace tel_bot_net.Controllers
{
    public class DbController : Controller
    {
        private BotDbContext db;

        public DbController(BotDbContext context)
        {
            db = context;
        }

        public async Task<OkResult> Create(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok();
        }

    }
}
