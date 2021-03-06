﻿// <auto-generated />
using System;
using BotDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BotDB.Migrations
{
    [DbContext(typeof(BotDbContext))]
    [Migration("20210303041035_Add_Initial_Data")]
    partial class Add_Initial_Data
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BotDB.DbModels.MyUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChatId = 420314247L,
                            Name = "Никитин Андрей",
                            PhoneNumber = "79138800573"
                        });
                });

            modelBuilder.Entity("BotDB.DbModels.Tool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InventoryNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tools");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            InventoryNumber = "101040031634",
                            Name = "Источник питания"
                        },
                        new
                        {
                            Id = 2,
                            InventoryNumber = "101040020762",
                            Name = "Источник питания"
                        },
                        new
                        {
                            Id = 3,
                            InventoryNumber = "101040020761",
                            Name = "Генератор сигналов"
                        },
                        new
                        {
                            Id = 4,
                            InventoryNumber = "101040020758",
                            Name = "Осциллограф"
                        },
                        new
                        {
                            Id = 5,
                            InventoryNumber = "101040020757",
                            Name = "Осциллограф"
                        },
                        new
                        {
                            Id = 6,
                            InventoryNumber = "101040020756",
                            Name = "Осциллограф"
                        },
                        new
                        {
                            Id = 7,
                            InventoryNumber = "101040020759",
                            Name = "Осциллограф"
                        },
                        new
                        {
                            Id = 8,
                            InventoryNumber = "101040020760",
                            Name = "Осциллограф"
                        },
                        new
                        {
                            Id = 9,
                            InventoryNumber = "101040031631",
                            Name = "Осциллограф"
                        },
                        new
                        {
                            Id = 10,
                            InventoryNumber = "101040031630",
                            Name = "Осциллограф"
                        },
                        new
                        {
                            Id = 11,
                            InventoryNumber = "101040031632",
                            Name = "Осциллограф"
                        },
                        new
                        {
                            Id = 12,
                            InventoryNumber = "101040020758",
                            Name = "Паяльная станция"
                        },
                        new
                        {
                            Id = 13,
                            InventoryNumber = "101040020757",
                            Name = "Паяльная станция"
                        },
                        new
                        {
                            Id = 14,
                            InventoryNumber = "101040031635",
                            Name = "Мультиметр"
                        },
                        new
                        {
                            Id = 15,
                            InventoryNumber = "101040031635",
                            Name = "Мультиметр"
                        },
                        new
                        {
                            Id = 16,
                            InventoryNumber = "101040031635",
                            Name = "Мультиметр"
                        },
                        new
                        {
                            Id = 17,
                            InventoryNumber = "101040020756",
                            Name = "Мультиметр"
                        },
                        new
                        {
                            Id = 18,
                            InventoryNumber = "нет",
                            Name = "Набор отвёрток"
                        },
                        new
                        {
                            Id = 19,
                            InventoryNumber = "нет",
                            Name = "Набор отвёрток"
                        });
                });

            modelBuilder.Entity("BotDB.DbModels.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateTimeClose")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTimeOpen")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageCloseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageOpenName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ToolId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ToolId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BotDB.DbModels.Transaction", b =>
                {
                    b.HasOne("BotDB.DbModels.Tool", "Tool")
                        .WithMany("Transactions")
                        .HasForeignKey("ToolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotDB.DbModels.MyUser", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tool");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BotDB.DbModels.MyUser", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BotDB.DbModels.Tool", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
