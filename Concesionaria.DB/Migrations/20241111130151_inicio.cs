using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concesionaria.DB.Migrations
{
    /// <inheritdoc />
    public partial class inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoDocumentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Motor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Combustible = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsEmpresa = table.Column<bool>(type: "bit", nullable: false),
                    NumDoc = table.Column<int>(type: "int", nullable: false),
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personas_TipoDocumentos_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Adjudicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaAdjudicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutoEntregado = table.Column<bool>(type: "bit", nullable: false),
                    PatenteVehiculo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adjudicaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adjudicaciones_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoPlanes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePlan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CantCuotas = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPlanes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipoPlanes_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CantPlanesVendidos = table.Column<int>(type: "int", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendedores_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanesVendidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FechaSuscripcion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaSorteo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanEnMora = table.Column<bool>(type: "bit", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    VendedorId = table.Column<int>(type: "int", nullable: false),
                    TipoPlanId = table.Column<int>(type: "int", nullable: false),
                    AdjudicacionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanesVendidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanesVendidos_Adjudicaciones_AdjudicacionId",
                        column: x => x.AdjudicacionId,
                        principalTable: "Adjudicaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlanesVendidos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanesVendidos_TipoPlanes_TipoPlanId",
                        column: x => x.TipoPlanId,
                        principalTable: "TipoPlanes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanesVendidos_Vendedores_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cuotas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorCuota = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NumeroCuota = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CuotaVencida = table.Column<bool>(type: "bit", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PlanVendidoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuotas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuotas_PlanesVendidos_PlanVendidoId",
                        column: x => x.PlanVendidoId,
                        principalTable: "PlanesVendidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MontoPagado = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodoPago = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReferenciaPago = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CuotaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagos_Cuotas_CuotaId",
                        column: x => x.CuotaId,
                        principalTable: "Cuotas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "Adjudicacion_UQ",
                table: "Adjudicaciones",
                column: "PatenteVehiculo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adjudicaciones_VehiculoId",
                table: "Adjudicaciones",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "PersonaId_UQ",
                table: "Clientes",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cuotas_PlanVendidoId",
                table: "Cuotas",
                column: "PlanVendidoId");

            migrationBuilder.CreateIndex(
                name: "CuotaId_UQ",
                table: "Pagos",
                column: "CuotaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Persona_Apellido_Nombre",
                table: "Personas",
                columns: new[] { "Apellido", "Nombre" });

            migrationBuilder.CreateIndex(
                name: "Persona_EsEmpresa",
                table: "Personas",
                column: "EsEmpresa");

            migrationBuilder.CreateIndex(
                name: "Persona_UQ",
                table: "Personas",
                columns: new[] { "TipoDocumentoId", "NumDoc" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ClienteId_UQ",
                table: "PlanesVendidos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "Estado",
                table: "PlanesVendidos",
                column: "Estado");

            migrationBuilder.CreateIndex(
                name: "IX_PlanesVendidos_AdjudicacionId",
                table: "PlanesVendidos",
                column: "AdjudicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanesVendidos_TipoPlanId",
                table: "PlanesVendidos",
                column: "TipoPlanId");

            migrationBuilder.CreateIndex(
                name: "VendedorId_UQ",
                table: "PlanesVendidos",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "TDocumento_UQ",
                table: "TipoDocumentos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoPlanes_VehiculoId",
                table: "TipoPlanes",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "NombrePlan_UQ",
                table: "TipoPlanes",
                column: "NombrePlan",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Codigo_UQ",
                table: "Vehiculos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "MArca_Modelo_UQ",
                table: "Vehiculos",
                columns: new[] { "Marca", "Modelo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PersonaId_UQ",
                table: "Vendedores",
                column: "PersonaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Cuotas");

            migrationBuilder.DropTable(
                name: "PlanesVendidos");

            migrationBuilder.DropTable(
                name: "Adjudicaciones");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "TipoPlanes");

            migrationBuilder.DropTable(
                name: "Vendedores");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "TipoDocumentos");
        }
    }
}
