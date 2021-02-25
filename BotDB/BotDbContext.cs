﻿//контекст базы данных

using Microsoft.EntityFrameworkCore;
using BotDB.DbModels;

namespace BotDB
{
    public class BotDbContext: DbContext
    {
        public DbSet<MyUser> Users { get; set; }// Таблица пользователей
        public DbSet<Tool> Tools { get; set; }//таблица инструментов
        public DbSet<OpenTransaction> OpenTransactions { get; set; }//таблица взятий инструментов
        public DbSet<CloseTransaction> CloseTransactions { get; set; }//таблица возвращений инструментов


        public BotDbContext()
        {
            //Database.EnsureCreated();// создаем базу данных при первом обращении
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=botDB;Trusted_Connection=True;");
        }
    }
}