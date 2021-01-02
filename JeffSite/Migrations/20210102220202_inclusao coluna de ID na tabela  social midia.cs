using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffSite.Migrations
{
    public partial class inclusaocolunadeIDnatabelasocialmidia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialMidia",
                table: "SocialMidia");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SocialMidia",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SocialMidia",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialMidia",
                table: "SocialMidia",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialMidia",
                table: "SocialMidia");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SocialMidia");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SocialMidia",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialMidia",
                table: "SocialMidia",
                column: "Name");
        }
    }
}
