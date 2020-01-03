using Microsoft.EntityFrameworkCore.Migrations;

namespace DuaControl.Web.Migrations
{
    public partial class AddingFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Facturas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Facturas");
        }
    }
}
