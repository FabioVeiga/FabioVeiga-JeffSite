using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffSite.Migrations
{
    public partial class ajustadoentidadeWhereToBuy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListWhereToBuy",
                table: "WhereToBuys");

            migrationBuilder.AddColumn<string>(
                name: "IconFA",
                table: "WhereToBuys",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WhereToBuys",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "WhereToBuys",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconFA",
                table: "WhereToBuys");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WhereToBuys");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "WhereToBuys");

            migrationBuilder.AddColumn<string>(
                name: "ListWhereToBuy",
                table: "WhereToBuys",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");
        }
    }
}
