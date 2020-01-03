using Microsoft.EntityFrameworkCore.Migrations;

namespace DuaControl.Web.Migrations
{
    public partial class completeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Clientes_ClientId",
                table: "Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Puertos_PortId",
                table: "Facturas");

            migrationBuilder.AlterColumn<int>(
                name: "PortId",
                table: "Facturas",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Facturas",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Clientes_ClientId",
                table: "Facturas",
                column: "ClientId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Puertos_PortId",
                table: "Facturas",
                column: "PortId",
                principalTable: "Puertos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Clientes_ClientId",
                table: "Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Puertos_PortId",
                table: "Facturas");

            migrationBuilder.AlterColumn<int>(
                name: "PortId",
                table: "Facturas",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Facturas",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Clientes_ClientId",
                table: "Facturas",
                column: "ClientId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Puertos_PortId",
                table: "Facturas",
                column: "PortId",
                principalTable: "Puertos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
