using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffSite.Migrations
{
    public partial class ajusterelacionamentowherecomlivro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdLivro",
                table: "WhereToBuys");

            migrationBuilder.AddColumn<int>(
                name: "LivroId",
                table: "WhereToBuys",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WhereToBuys_LivroId",
                table: "WhereToBuys",
                column: "LivroId");

            migrationBuilder.AddForeignKey(
                name: "FK_WhereToBuys_Livros_LivroId",
                table: "WhereToBuys",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WhereToBuys_Livros_LivroId",
                table: "WhereToBuys");

            migrationBuilder.DropIndex(
                name: "IX_WhereToBuys_LivroId",
                table: "WhereToBuys");

            migrationBuilder.DropColumn(
                name: "LivroId",
                table: "WhereToBuys");

            migrationBuilder.AddColumn<int>(
                name: "IdLivro",
                table: "WhereToBuys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
