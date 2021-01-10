using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffSite.Migrations
{
    public partial class asjutenatabelaleitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Leitors");

            migrationBuilder.AddColumn<string>(
                name: "NameImg",
                table: "Leitors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathImg",
                table: "Leitors",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameImg",
                table: "Leitors");

            migrationBuilder.DropColumn(
                name: "PathImg",
                table: "Leitors");

            migrationBuilder.AddColumn<byte[]>(
                name: "Img",
                table: "Leitors",
                type: "longblob",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
