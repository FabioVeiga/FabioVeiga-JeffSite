using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffSite.Migrations
{
    public partial class addondeeaniversario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Birthday",
                table: "Mallings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Onde",
                table: "Mallings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Mallings");

            migrationBuilder.DropColumn(
                name: "Onde",
                table: "Mallings");
        }
    }
}
