using Microsoft.EntityFrameworkCore.Migrations;

namespace DuaControl.Web.Migrations
{
    public partial class ModifyingDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sistema",
                table: "Facturas",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Facturas",
                maxLength: 15,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sistema",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Facturas");
        }
    }
}
