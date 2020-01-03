using Microsoft.EntityFrameworkCore.Migrations;

namespace DuaControl.Web.Migrations
{
    public partial class ModifyDB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Facturas_FacturaId",
                table: "Agenda");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agenda",
                table: "Agenda");

            migrationBuilder.RenameTable(
                name: "Agenda",
                newName: "Adjuntos");

            migrationBuilder.RenameIndex(
                name: "IX_Agenda_FacturaId",
                table: "Adjuntos",
                newName: "IX_Adjuntos_FacturaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adjuntos",
                table: "Adjuntos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adjuntos_Facturas_FacturaId",
                table: "Adjuntos",
                column: "FacturaId",
                principalTable: "Facturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adjuntos_Facturas_FacturaId",
                table: "Adjuntos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adjuntos",
                table: "Adjuntos");

            migrationBuilder.RenameTable(
                name: "Adjuntos",
                newName: "Agenda");

            migrationBuilder.RenameIndex(
                name: "IX_Adjuntos_FacturaId",
                table: "Agenda",
                newName: "IX_Agenda_FacturaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agenda",
                table: "Agenda",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Facturas_FacturaId",
                table: "Agenda",
                column: "FacturaId",
                principalTable: "Facturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
