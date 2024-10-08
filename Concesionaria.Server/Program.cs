using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Servicios de los repositorios

builder.Services.AddScoped<ITipoDocumentoRepositorio, TipoDocumentoRepositorio>();
builder.Services.AddScoped<IRepositorio<Persona>, Repositorio<Persona>>();
builder.Services.AddScoped<IRepositorio<PlanVendido>, Repositorio<PlanVendido>>();
builder.Services.AddScoped<IVendedorRepositorio, VendedorRepositorio>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IRepositorio<Cuota>, Repositorio<Cuota>>();
builder.Services.AddScoped<IRepositorio<Pago>, Repositorio<Pago>>();

// Coneccion con la BD

builder.Services.AddDbContext<Context>(op => op.UseSqlServer("name=conn"));

// AutoMapper

builder.Services.AddAutoMapper(typeof(Program));

//=================================================

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
