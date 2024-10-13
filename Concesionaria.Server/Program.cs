using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.AndresRepositorios;
using Concesionaria.Server.Repositorio.FacundoRepositorios;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Servicios de los repositorios

builder.Services.AddScoped<ITipoDocumentoRepositorio, TipoDocumentoRepositorio>();
builder.Services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
builder.Services.AddScoped<IPlanVendidoRepositorio, PlanVendidoRepositorio>();
builder.Services.AddScoped<IVendedorRepositorio, VendedorRepositorio>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IRepositorio<Cuota>, Repositorio<Cuota>>();
builder.Services.AddScoped<IRepositorio<Pago>, Repositorio<Pago>>();
builder.Services.AddScoped<IRepositorio<Adjudicacion>, Repositorio<Adjudicacion>>();
builder.Services.AddScoped<IRepositorio<TipoPlan>, Repositorio<TipoPlan>>();
builder.Services.AddScoped<IRepositorio<Vehiculo>, Repositorio<Vehiculo>>();

// Coneccion con la BD / Context

builder.Services.AddDbContext<Context>(op => op.UseSqlServer("name=conn"));

// AutoMapper

builder.Services.AddAutoMapper(typeof(Program));

// =================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
