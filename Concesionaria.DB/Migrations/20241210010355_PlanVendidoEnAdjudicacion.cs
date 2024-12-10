using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concesionaria.DB.Migrations
{
    /// <inheritdoc />
    public partial class PlanVendidoEnAdjudicacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanesVendidos_Adjudicaciones_AdjudicacionId",
                table: "PlanesVendidos");

            migrationBuilder.DropIndex(
                name: "IX_PlanesVendidos_AdjudicacionId",
                table: "PlanesVendidos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaSuscripcion",
                table: "PlanesVendidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaSuscripcion",
                table: "PlanesVendidos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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
    }
}
