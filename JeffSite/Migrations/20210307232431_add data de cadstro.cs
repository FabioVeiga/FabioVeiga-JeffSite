using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffSite.Migrations
{
    public partial class adddatadecadstro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Mallings");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAniversario",
                table: "Mallings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Mallings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAniversario",
                table: "Mallings");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Mallings");

            migrationBuilder.AddColumn<string>(
                name: "Birthday",
                table: "Mallings",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
