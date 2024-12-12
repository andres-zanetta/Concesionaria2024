using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concesionaria.DB.Migrations
{
    /// <inheritdoc />
    public partial class DeleteMontoPagadoPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontoPagado",
                table: "Pagos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MontoPagado",
                table: "Pagos",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
