﻿// <auto-generated />
using System;
using Concesionaria.DB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Concesionaria.DB.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20241212011427_RelacionCuotaPago1a1")]
    partial class RelacionCuotaPago1a1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Adjudicacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AutoEntregado")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Detalle")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("FechaAdjudicacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("PatenteVehiculo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("PlanVendidoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlanVendidoId")
                        .IsUnique();

                    b.HasIndex(new[] { "PatenteVehiculo" }, "Adjudicacion_UQ")
                        .IsUnique();

                    b.ToTable("Adjudicaciones");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("PersonaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "PersonaId" }, "PersonaId_UQ")
                        .IsUnique();

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Cuota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("CuotaVencida")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumeroCuota")
                        .HasColumnType("int");

                    b.Property<string>("Observaciones")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PlanVendidoId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorCuota")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("PlanVendidoId");

                    b.ToTable("Cuotas");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Pago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CuotaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("datetime2");

                    b.Property<string>("MetodoPago")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("MontoPagado")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("ReferenciaPago")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CuotaId" }, "CuotaId_UQ")
                        .IsUnique();

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EsEmpresa")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NumDoc")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoDocumentoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Apellido", "Nombre" }, "Persona_Apellido_Nombre");

                    b.HasIndex(new[] { "EsEmpresa" }, "Persona_EsEmpresa");

                    b.HasIndex(new[] { "TipoDocumentoId", "NumDoc" }, "Persona_UQ")
                        .IsUnique();

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.PlanVendido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AdjudicacionId")
                        .HasColumnType("int");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaSorteo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaSuscripcion")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PlanEnMora")
                        .HasColumnType("bit");

                    b.Property<int>("TipoPlanId")
                        .HasColumnType("int");

                    b.Property<int>("VendedorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoPlanId");

                    b.HasIndex(new[] { "ClienteId" }, "ClienteId_UQ");

                    b.HasIndex(new[] { "Estado" }, "Estado");

                    b.HasIndex(new[] { "VendedorId" }, "VendedorId_UQ");

                    b.ToTable("PlanesVendidos");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.TipoDocumento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Codigo" }, "TDocumento_UQ")
                        .IsUnique();

                    b.ToTable("TipoDocumentos");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.TipoPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantCuotas")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombrePlan")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("VehiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehiculoId");

                    b.HasIndex(new[] { "NombrePlan" }, "NombrePlan_UQ")
                        .IsUnique();

                    b.ToTable("TipoPlanes");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Vehiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Combustible")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Motor")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Codigo" }, "Codigo_UQ")
                        .IsUnique();

                    b.HasIndex(new[] { "Marca", "Modelo" }, "MArca_Modelo_UQ")
                        .IsUnique();

                    b.ToTable("Vehiculos");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Vendedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CantPlanesVendidos")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("PersonaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "PersonaId" }, "PersonaId_UQ")
                        .IsUnique();

                    b.ToTable("Vendedores");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Adjudicacion", b =>
                {
                    b.HasOne("Concesionaria.DB.Data.Entidades.PlanVendido", "PlanVendido")
                        .WithOne("Adjudicacion")
                        .HasForeignKey("Concesionaria.DB.Data.Entidades.Adjudicacion", "PlanVendidoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PlanVendido");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Cliente", b =>
                {
                    b.HasOne("Concesionaria.DB.Data.Entidades.Persona", "Persona")
                        .WithOne("Cliente")
                        .HasForeignKey("Concesionaria.DB.Data.Entidades.Cliente", "PersonaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Cuota", b =>
                {
                    b.HasOne("Concesionaria.DB.Data.Entidades.PlanVendido", "PlanVendido")
                        .WithMany("cuotas")
                        .HasForeignKey("PlanVendidoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PlanVendido");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Pago", b =>
                {
                    b.HasOne("Concesionaria.DB.Data.Entidades.Cuota", "Cuota")
                        .WithOne("Pago")
                        .HasForeignKey("Concesionaria.DB.Data.Entidades.Pago", "CuotaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cuota");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Persona", b =>
                {
                    b.HasOne("Concesionaria.DB.Data.Entidades.TipoDocumento", "TipoDocumento")
                        .WithMany("Personas")
                        .HasForeignKey("TipoDocumentoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TipoDocumento");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.PlanVendido", b =>
                {
                    b.HasOne("Concesionaria.DB.Data.Entidades.Cliente", "Cliente")
                        .WithMany("planVendidos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Concesionaria.DB.Data.Entidades.TipoPlan", "TipoPlan")
                        .WithMany()
                        .HasForeignKey("TipoPlanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Concesionaria.DB.Data.Entidades.Vendedor", "Vendedor")
                        .WithMany("planVendidos")
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("TipoPlan");

                    b.Navigation("Vendedor");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.TipoPlan", b =>
                {
                    b.HasOne("Concesionaria.DB.Data.Entidades.Vehiculo", "Vehiculo")
                        .WithMany("TipoPlanes")
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Vehiculo");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Vendedor", b =>
                {
                    b.HasOne("Concesionaria.DB.Data.Entidades.Persona", "Persona")
                        .WithOne("Vendedor")
                        .HasForeignKey("Concesionaria.DB.Data.Entidades.Vendedor", "PersonaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Cliente", b =>
                {
                    b.Navigation("planVendidos");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Cuota", b =>
                {
                    b.Navigation("Pago")
                        .IsRequired();
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Persona", b =>
                {
                    b.Navigation("Cliente")
                        .IsRequired();

                    b.Navigation("Vendedor")
                        .IsRequired();
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.PlanVendido", b =>
                {
                    b.Navigation("Adjudicacion");

                    b.Navigation("cuotas");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.TipoDocumento", b =>
                {
                    b.Navigation("Personas");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Vehiculo", b =>
                {
                    b.Navigation("TipoPlanes");
                });

            modelBuilder.Entity("Concesionaria.DB.Data.Entidades.Vendedor", b =>
                {
                    b.Navigation("planVendidos");
                });
#pragma warning restore 612, 618
        }
    }
}