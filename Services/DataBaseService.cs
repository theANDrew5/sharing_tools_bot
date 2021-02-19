using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tel_bot_net.Models;
using tel_bot_net.Models.DbModels;

namespace tel_bot_net.Services
{
    public class DataBaseService
    {
        public async Task<bool> AddUser(MyUser user)
        {
            using (var db = new BotDbContext())
            {
                await db.Users.AddAsync(user);
                if (db.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }

        }

    }
}
