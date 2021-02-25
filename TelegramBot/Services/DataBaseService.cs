using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotDB;
using BotDB.DbModels;

namespace tel_bot_net.Services
{
    public class DataBaseService
    {
        public async Task<bool> AddUser(MyUser user)
        {
            using (var db = new BotDbContext())
            {
                db.Users.Add(user);
                if (await db.SaveChangesAsync() > 0)
                    return true;
                else
                    return false;
            }

        }

        //public async  Task<List<long>> getUsersList()
        //{
        //    using (var db = new BotDbContext())
        //    {
        //        db.Users.
        //    }
        //}

        public bool UserCheck (long chatID)
        {
            using (var db = new BotDbContext())
            {
                //var userId = from user in db.Users.
                try
                {
                    //int userId = db.Users.Where(u => u.ChatId == chatID).Select(u => u.Id).Single();
                    if (db.Users
                        .Where(u => u.ChatId == chatID)
                        .Select(u => u.Id).Single() > 0)
                        return true;
                    return false;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }
        }

    }
}
