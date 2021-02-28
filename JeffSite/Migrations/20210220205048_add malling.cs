using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffSite.Migrations
{
    public partial class addmalling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mallings",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mallings", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mallings");
        }
    }
}
