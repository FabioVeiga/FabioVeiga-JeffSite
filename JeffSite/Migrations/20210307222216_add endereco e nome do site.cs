using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffSite.Migrations
{
    public partial class addenderecoenomedosite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeSite",
                table: "Configuracao",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlSite",
                table: "Configuracao",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeSite",
                table: "Configuracao");

            migrationBuilder.DropColumn(
                name: "UrlSite",
                table: "Configuracao");
        }
    }
}
