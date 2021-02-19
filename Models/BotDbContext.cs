//контекст базы данных

using Microsoft.EntityFrameworkCore;
using tel_bot_net.Models.DbModels;

namespace tel_bot_net.Models
{
    public class BotDbContext: DbContext
    {
        public DbSet<MyUser> Users { get; set; }// Таблица пользователей

        //public BotDbContext(DbContextOptions<BotDbContext> options)
        //    : base(options)
        //{
        //    Database.EnsureCreated();// создаем базу данных при первом обращении
        //}

        public BotDbContext()
        {
            Database.EnsureCreated();// создаем базу данных при первом обращении
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=botDB;Trusted_Connection=True;");
        }
    }
}
