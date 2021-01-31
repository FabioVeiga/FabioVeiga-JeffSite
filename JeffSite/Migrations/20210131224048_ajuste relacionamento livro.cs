using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffSite.Migrations
{
    public partial class ajusterelacionamentolivro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Tags_TagId",
                table: "Livros");

            migrationBuilder.DropForeignKey(
                name: "FK_Livros_WhereToBuys_WhereToBuyId",
                table: "Livros");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Livros_TagId",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_WhereToBuyId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "WhereToBuyId",
                table: "Livros");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Livros",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WhereToBuyId",
                table: "Livros",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdLivro = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livros_TagId",
                table: "Livros",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_WhereToBuyId",
                table: "Livros",
                column: "WhereToBuyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Tags_TagId",
                table: "Livros",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_WhereToBuys_WhereToBuyId",
                table: "Livros",
                column: "WhereToBuyId",
                principalTable: "WhereToBuys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
