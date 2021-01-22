using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OsirisTest.Data.Migrations
{
    public partial class AddLastEmailDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastEmailDateTime",
                table: "Customer",
                type: "datetime",
                nullable: true,
                defaultValueSql: "NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastEmailDateTime",
                table: "Customer");
        }
    }
}
