using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BotDB.DbModels;


namespace BotDB
{


    public class DBMethods
    {

#if DEBUG
        public void InitialBase()
        {
            using (var db = new BotDbContext())
            {
                //db.Users.Add(new MyUser { })
                db.Tools.AddRange(new List<Tool>
                {
                    new Tool {Name = "Осциллограф", InventoryNumber="123"},
                    new Tool {Name = "Осциллограф", InventoryNumber="124"},
                    new Tool {Name = "Мультиметр", InventoryNumber="125"},
                    new Tool {Name = "Мультиметр", InventoryNumber="126"},
                    new Tool {Name = "Ящик с инструментами", InventoryNumber="127"}
                });

                db.SaveChanges();
            }
        }

        public MyUser GetUser (int userId)
        {
            using (var db = new BotDbContext())
            {
                try
                {
                    return db.Users
                        .Include(u=>u.Transactions)
                        .Where(u => u.Id == userId)
                        .Single();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }
#endif


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
                    return db.Users
                        .Include(u=>u.Transactions)
                        .Where(user => user.ChatId == chatID)
                        .Single();
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
                    return db.Tools
                        .Include(t=>t.Transactions)
                        .Where(tool => tool.Id == toolId)
                        .Single();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }


        public bool OpenTransaction(MyUser _user, Tool _tool)
        {
            using (var db = new BotDbContext())
            {
                Transaction transaction = new Transaction { DateTimeOpen = DateTime.Now };

                var user = db.Users.Include(u => u.Transactions).Where(u => u.Id == _user.Id).Single();
                var tool = db.Tools.Include(t => t.Transactions).Where(t => t.Id == _tool.Id).Single();

                user.Transactions.Add(transaction);
                tool.Transactions.Add(transaction);

                if (db.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool CloseTransaction(MyUser user, Tool tool)
        {
            using (var db = new BotDbContext())
            {
                Transaction transaction = db.Transactions
                                            .Where(t => t.UserId == user.Id && t.ToolId == tool.Id)
                                            .SingleOrDefault();
                if (transaction != null)
                {
                    transaction.DateTimeClose = DateTime.Now;
                    if (db.SaveChanges() > 0)
                        return true;
                }
                return false;
            }
        }

        public Transaction GetOpenTransaction (int toolId)
        {
            using (var db = new BotDbContext())
            {
                    return db.Transactions
                        .Where(t => t.ToolId == toolId && t.DateTimeClose != null)
                        .SingleOrDefault();
            }
        }

        public List<Transaction> GetOpenTransactions(long chatId)
        {
            using (var db = new BotDbContext())
            {
                return db.Transactions
                    .Include(t=>t.Tool)
                    .Where(t => t.User.ChatId == chatId && t.DateTimeClose == null)
                    .ToList();
            }
        }

    }
}
