using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OsirisTest.Data.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EmailAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: false),
                    FirstDepositAmount = table.Column<decimal>(type: "decimal(19,5)", nullable: true),
                    LastWagerAmount = table.Column<decimal>(type: "decimal(19,5)", nullable: true),
                    LastWagerDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    InsertedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdateDateTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Wager",
                columns: table => new
                {
                    WagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(19,5)", nullable: true),
                    WagerDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    InsertedDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wager", x => x.WagerId);
                    table.ForeignKey(
                        name: "FK_Wager_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wager_CustomerId",
                table: "Wager",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wager");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
