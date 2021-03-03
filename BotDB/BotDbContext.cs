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

            modelBuilder.Entity<MyUser>()
                .HasData
                (new MyUser { Id = 1, Name = "Никитин Андрей", ChatId = 420314247, PhoneNumber = "79138800573" });

            modelBuilder.Entity<Tool>()
                .HasData
                (new Tool[]
                {
                    new Tool { Id = 1 , Name = "Источник питания", InventoryNumber = "101040031634"},
                    new Tool { Id = 2 , Name = "Источник питания", InventoryNumber = "101040020762"},
                    new Tool { Id = 3 , Name = "Генератор сигналов", InventoryNumber = "101040020761"},
                    new Tool { Id = 4 , Name = "Осциллограф", InventoryNumber = "101040020758"},
                    new Tool { Id = 5 , Name = "Осциллограф", InventoryNumber = "101040020757"},
                    new Tool { Id = 6 , Name = "Осциллограф", InventoryNumber = "101040020756"},
                    new Tool { Id = 7 , Name = "Осциллограф", InventoryNumber = "101040020759"},
                    new Tool { Id = 8 , Name = "Осциллограф", InventoryNumber = "101040020760"},
                    new Tool { Id = 9 , Name = "Осциллограф", InventoryNumber = "101040031631"},
                    new Tool { Id = 10 , Name = "Осциллограф", InventoryNumber = "101040031630"},
                    new Tool { Id = 11 , Name = "Осциллограф", InventoryNumber = "101040031632"},
                    new Tool { Id = 12 , Name = "Паяльная станция", InventoryNumber = "101040020758"},
                    new Tool { Id = 13 , Name = "Паяльная станция", InventoryNumber = "101040020757"},
                    new Tool { Id = 14 , Name = "Мультиметр", InventoryNumber = "101040031635"},
                    new Tool { Id = 15 , Name = "Мультиметр", InventoryNumber = "101040031635"},
                    new Tool { Id = 16 , Name = "Мультиметр", InventoryNumber = "101040031635"},
                    new Tool { Id = 17 , Name = "Мультиметр", InventoryNumber = "101040020756"},
                    new Tool { Id = 18 , Name = "Набор отвёрток", InventoryNumber = "нет"},
                    new Tool { Id = 19 , Name = "Набор отвёрток", InventoryNumber = "нет"}
                });
        }
    }
}
