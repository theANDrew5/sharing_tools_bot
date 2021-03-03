using Microsoft.EntityFrameworkCore.Migrations;

namespace BotDB.Migrations
{
    public partial class Add_Initial_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tools",
                columns: new[] { "Id", "InventoryNumber", "Name" },
                values: new object[,]
                {
                    { 9, "101040031631", "Осциллограф" },
                    { 17, "101040020756", "Мультиметр" },
                    { 16, "101040031635", "Мультиметр" },
                    { 15, "101040031635", "Мультиметр" },
                    { 14, "101040031635", "Мультиметр" },
                    { 13, "101040020757", "Паяльная станция" },
                    { 12, "101040020758", "Паяльная станция" },
                    { 11, "101040031632", "Осциллограф" },
                    { 10, "101040031630", "Осциллограф" },
                    { 19, "нет", "Набор отвёрток" },
                    { 8, "101040020760", "Осциллограф" },
                    { 7, "101040020759", "Осциллограф" },
                    { 6, "101040020756", "Осциллограф" },
                    { 5, "101040020757", "Осциллограф" },
                    { 4, "101040020758", "Осциллограф" },
                    { 3, "101040020761", "Генератор сигналов" },
                    { 2, "101040020762", "Источник питания" },
                    { 1, "101040031634", "Источник питания" },
                    { 18, "нет", "Набор отвёрток" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ChatId", "Name", "PhoneNumber" },
                values: new object[] { 1, 420314247L, "Никитин Андрей", "79138800573" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
