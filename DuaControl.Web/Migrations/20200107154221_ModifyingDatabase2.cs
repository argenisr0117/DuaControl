using Microsoft.EntityFrameworkCore.Migrations;

namespace DuaControl.Web.Migrations
{
    public partial class ModifyingDatabase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User",
                table: "Facturas",
                newName: "InvoiceUser");

            migrationBuilder.RenameColumn(
                name: "Sistema",
                table: "Facturas",
                newName: "InvoiceSystem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceUser",
                table: "Facturas",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "InvoiceSystem",
                table: "Facturas",
                newName: "Sistema");
        }
    }
}
