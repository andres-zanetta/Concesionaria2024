using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concesionaria.DB.Migrations
{
    /// <inheritdoc />
    public partial class CambioFKenPlanVyAdjudic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adjudicaciones_PlanesVendidos_PlanVendidoId",
                table: "Adjudicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Adjudicaciones_PlanVendidoId",
                table: "Adjudicaciones");

            migrationBuilder.DropColumn(
                name: "PlanVendidoId",
                table: "Adjudicaciones");

            migrationBuilder.AddColumn<int>(
                name: "AdjudicacionId",
                table: "PlanesVendidos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanesVendidos_AdjudicacionId",
                table: "PlanesVendidos",
                column: "AdjudicacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanesVendidos_Adjudicaciones_AdjudicacionId",
                table: "PlanesVendidos",
                column: "AdjudicacionId",
                principalTable: "Adjudicaciones",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanesVendidos_Adjudicaciones_AdjudicacionId",
                table: "PlanesVendidos");

            migrationBuilder.DropIndex(
                name: "IX_PlanesVendidos_AdjudicacionId",
                table: "PlanesVendidos");

            migrationBuilder.DropColumn(
                name: "AdjudicacionId",
                table: "PlanesVendidos");

            migrationBuilder.AddColumn<int>(
                name: "PlanVendidoId",
                table: "Adjudicaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Adjudicaciones_PlanVendidoId",
                table: "Adjudicaciones",
                column: "PlanVendidoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Adjudicaciones_PlanesVendidos_PlanVendidoId",
                table: "Adjudicaciones",
                column: "PlanVendidoId",
                principalTable: "PlanesVendidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
