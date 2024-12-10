using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concesionaria.DB.Migrations
{
    /// <inheritdoc />
    public partial class DelVehiculoId_Adjudic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adjudicaciones_Vehiculos_VehiculoId",
                table: "Adjudicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Adjudicaciones_VehiculoId",
                table: "Adjudicaciones");

            migrationBuilder.DropColumn(
                name: "VehiculoId",
                table: "Adjudicaciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehiculoId",
                table: "Adjudicaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Adjudicaciones_VehiculoId",
                table: "Adjudicaciones",
                column: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adjudicaciones_Vehiculos_VehiculoId",
                table: "Adjudicaciones",
                column: "VehiculoId",
                principalTable: "Vehiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
