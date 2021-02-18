using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffSite.Migrations
{
    public partial class addcolunas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Pedidos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Pedidos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Pedidos");
        }
    }
}
