using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffSite.Migrations
{
    public partial class addconfiguracaoemail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdLivro",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "LivroId",
                table: "Pedidos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Servidor = table.Column<string>(nullable: false),
                    Porta = table.Column<int>(nullable: false),
                    ContaEmail = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(nullable: false),
                    HabilitaSSL = table.Column<bool>(nullable: false),
                    UsarCredencialPadrao = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropColumn(
                name: "LivroId",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "IdLivro",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
