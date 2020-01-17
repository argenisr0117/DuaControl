using Microsoft.EntityFrameworkCore.Migrations;

namespace DuaControl.Web.Migrations
{
    public partial class ModifyingDatabase3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Clientes_ClienteId",
                table: "Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Puertos_PortId",
                table: "Facturas");

            migrationBuilder.AlterColumn<int>(
                name: "PortId",
                table: "Facturas",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ClienteId",
                table: "Facturas",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Adjuntos",
                type: "VARCHAR(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentName",
                table: "Adjuntos",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Clientes_ClienteId",
                table: "Facturas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Puertos_PortId",
                table: "Facturas",
                column: "PortId",
                principalTable: "Puertos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Clientes_ClienteId",
                table: "Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Puertos_PortId",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "DocumentName",
                table: "Adjuntos");

            migrationBuilder.AlterColumn<int>(
                name: "PortId",
                table: "Facturas",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClienteId",
                table: "Facturas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Adjuntos",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Clientes_ClienteId",
                table: "Facturas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Puertos_PortId",
                table: "Facturas",
                column: "PortId",
                principalTable: "Puertos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
