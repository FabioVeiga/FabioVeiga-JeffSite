using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffSite.Migrations
{
    public partial class Livroeentidades2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Tags_TagsId",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_TagsId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "TagsId",
                table: "Livros");

            migrationBuilder.AddColumn<int>(
                name: "IdLivro",
                table: "WhereToBuys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdLivro",
                table: "Tags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Livros",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Livros_TagId",
                table: "Livros",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Tags_TagId",
                table: "Livros",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Tags_TagId",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_TagId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "IdLivro",
                table: "WhereToBuys");

            migrationBuilder.DropColumn(
                name: "IdLivro",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Livros");

            migrationBuilder.AddColumn<int>(
                name: "TagsId",
                table: "Livros",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Livros_TagsId",
                table: "Livros",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Tags_TagsId",
                table: "Livros",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
