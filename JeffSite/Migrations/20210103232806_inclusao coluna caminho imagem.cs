using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffSite.Migrations
{
    public partial class inclusaocolunacaminhoimagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Carousels",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Carousels",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathImage",
                table: "Carousels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathImage",
                table: "Carousels");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Carousels",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Carousels",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
