using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BotDB.Migrations
{
    public partial class One_Transaction_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CloseTransactions");

            migrationBuilder.DropTable(
                name: "OpenTransactions");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    ImageOpenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTimeOpen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageCloseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTimeClose = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ToolId",
                table: "Transactions",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.CreateTable(
                name: "OpenTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeOpen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    ToolIds = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenTransactions_Tools_ToolIds",
                        column: x => x.ToolIds,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpenTransactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CloseTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeClose = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenTransactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloseTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CloseTransactions_OpenTransactions_OpenTransactionId",
                        column: x => x.OpenTransactionId,
                        principalTable: "OpenTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CloseTransactions_OpenTransactionId",
                table: "CloseTransactions",
                column: "OpenTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenTransactions_ToolIds",
                table: "OpenTransactions",
                column: "ToolIds");

            migrationBuilder.CreateIndex(
                name: "IX_OpenTransactions_UserId",
                table: "OpenTransactions",
                column: "UserId");
        }
    }
}
