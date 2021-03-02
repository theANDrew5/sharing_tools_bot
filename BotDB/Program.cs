using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BotDB.DbModels;


namespace BotDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //using (var db = new BotDbContext())
            //{
            //    MyUser user = db.Users.FirstOrDefault();
            //    Tool tool1 = db.Tools.Where(t => t.Id == 3).Single();
            //    Tool tool2 = db.Tools.Where(t => t.Id == 2).Single();

            //    Transaction tr1 = new Transaction { User = user, Tool = tool1, DateTimeOpen = DateTime.Now, };
            //    Transaction tr2 = new Transaction { User = user, Tool = tool2, DateTimeOpen = DateTime.Now, };

            //    db.Transactions.Add(tr1);
            //    if (db.SaveChanges() > 0)
            //    { }

            //    MyUser usr = db.Users.Include(u => u.Transactions).Where(u => u.Id == 1).Single();

            //    usr.Transactions[0].DateTimeClose = DateTime.Now;
            //    usr.Transactions[1].DateTimeClose = DateTime.Now;

            //    db.SaveChanges();

            //    foreach (var tr in usr.Transactions)
            //    {
            //        if (tr.DateTimeClose == null)
            //            Console.WriteLine($"{tr.Id}\t{tr.UserId}\t{tr.ToolId}\t{tr.DateTimeOpen}");
            //    }

            //    List<Tool> tools = db.Tools.Include(tool => tool.Transactions).ToList();
            //    List<MyUser> myUsers = db.Users.Include(user => user.Transactions).ToList();
            //    List<Transaction> transactions = db.Transactions.ToList();
            //}    

            var db = DBMethods.GetInstance();


            Console.WriteLine("OK");
        }
    }
}
