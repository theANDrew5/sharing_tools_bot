//контекст базы данных

using Microsoft.EntityFrameworkCore;
using tel_bot_net.Models.DbModels;

namespace tel_bot_net.Models
{
    public class BotDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }// Таблица пользователей

        public BotDbContext(DbContextOptions<BotDbContext> options)
            :base(options)
        {
            Database.EnsureCreated();// создаем базу данных при первом обращении
        }
    }
}
