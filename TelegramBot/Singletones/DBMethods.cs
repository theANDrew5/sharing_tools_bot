using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BotDB;
using BotDB.DbModels;


namespace TelegramBot.Singletones
{
    public class DBMethods
    {
        private static readonly DBMethods instance = new DBMethods();

        private DBMethods()
        { }

        public static DBMethods GetInstance()
        {
            return instance;
        }

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
        
        public MyUser GetUser (long chatID)
        {
            using (var db = new BotDbContext())
            {
                try
                {
                    return db.Users.
                        Where(user => user.ChatId == chatID).
                        Single();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }    
            }
        }

        public Tool GetTool (int toolId)
        {
            using (var db = new BotDbContext())
            {
                try
                {
                    return db.Tools.
                        Where(tool => tool.Id == toolId).
                        Single();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }

        public bool AddTransaction(long userId, int toolId)
        {
            using (var db = new BotDbContext())
            {
                MyUser user = GetUser(userId);
                Tool tool = db.Tools.Where(t => t.Id == toolId).Single();
                Transaction transaction = new Transaction { User = user, UserId=user.Id, Tool = tool, ToolId=tool.Id, DateTimeOpen = DateTime.Now };
                db.Transactions.Add(transaction);
                if (db.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
