//контекст базы данных

using Microsoft.EntityFrameworkCore;
using BotDB.DbModels;

namespace BotDB
{
    public class BotDbContext: DbContext
    {
        public DbSet<MyUser> Users { get; set; }// Таблица пользователей
        public DbSet<Tool> Tools { get; set; }//таблица инструментов
        public DbSet<Transaction> Transactions { get; set; }//таблица взятий инструментов


        public BotDbContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();// создаем базу данных при первом обращении
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=botDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t=>t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Tool)
                .WithMany(to => to.Transactions)
                .HasForeignKey(t=>t.ToolId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MyUser>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Tool>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.Id);
        }
    }
}
