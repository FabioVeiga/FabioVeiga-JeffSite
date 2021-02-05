using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffSite.Migrations
{
    public partial class alteracolunaurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "WhereToBuys");

            migrationBuilder.AddColumn<string>(
                name: "UrlEndereco",
                table: "WhereToBuys",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlEndereco",
                table: "WhereToBuys");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "WhereToBuys",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
