using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concesionaria.DB.Migrations
{
    /// <inheritdoc />
    public partial class ModifRelacionCL_PV_Temporal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanesVendidos_TipoPlanes_TipoPlanId",
                table: "PlanesVendidos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanesVendidos_Vendedores_VendedorId",
                table: "PlanesVendidos");

            migrationBuilder.AlterColumn<int>(
                name: "VendedorId",
                table: "PlanesVendidos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TipoPlanId",
                table: "PlanesVendidos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanesVendidos_TipoPlanes_TipoPlanId",
                table: "PlanesVendidos",
                column: "TipoPlanId",
                principalTable: "TipoPlanes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanesVendidos_Vendedores_VendedorId",
                table: "PlanesVendidos",
                column: "VendedorId",
                principalTable: "Vendedores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanesVendidos_TipoPlanes_TipoPlanId",
                table: "PlanesVendidos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanesVendidos_Vendedores_VendedorId",
                table: "PlanesVendidos");

            migrationBuilder.AlterColumn<int>(
                name: "VendedorId",
                table: "PlanesVendidos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoPlanId",
                table: "PlanesVendidos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanesVendidos_TipoPlanes_TipoPlanId",
                table: "PlanesVendidos",
                column: "TipoPlanId",
                principalTable: "TipoPlanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanesVendidos_Vendedores_VendedorId",
                table: "PlanesVendidos",
                column: "VendedorId",
                principalTable: "Vendedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
