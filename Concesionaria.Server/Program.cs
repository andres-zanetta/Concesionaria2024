using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.AndresRepositorios;
using Concesionaria.Server.Repositorio.BrunoRepositorios;
using Concesionaria.Server.Repositorio.FacundoRepositorios;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria.Server.Resolvers.ClienteResolver;
using Concesionaria.Server.Resolvers.PersonaResolvers;
using Concesionaria.Server.Resolvers.TipoPlanResolvers;
using Concesionaria.Server.Resolvers.VendedorResolver;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x=>x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Servicio para el Client

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Servicios de los repositorios

builder.Services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));   // <-- necesario para ejecutar el resolver.
builder.Services.AddScoped<ITipoDocumentoRepositorio, TipoDocumentoRepositorio>();
builder.Services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
builder.Services.AddScoped<IPlanVendidoRepositorio, PlanVendidoRepositorio>();
builder.Services.AddScoped<IVendedorRepositorio, VendedorRepositorio>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<ICuotaRepositorio, CuotaRepositorio>();
builder.Services.AddScoped<IPagoRepositorio, PagoRepositorio>();
builder.Services.AddScoped<IRepositorio<Adjudicacion>, AdjudicacionRepositorio>();
builder.Services.AddScoped<ITipoPlanRepositorio, TipoPlanRepositorio>();
builder.Services.AddScoped<IVehiculoRepositorio, VehiculoRepositorio>();

// Resolvers

builder.Services.AddScoped<TipoDocumentoResolverPost>();
builder.Services.AddScoped<TipoDocumentoResolverPut>();  // resolver linea 45 y 46 son de persona. Trackean id de tipo doc.
builder.Services.AddScoped<TipoPlanResolverPost>();
builder.Services.AddScoped<TipoPlanResolverPut>();
builder.Services.AddScoped<PersonaResolverPost>();
builder.Services.AddScoped<PersonaResolverPut>();
builder.Services.AddScoped<PersonaVendedorResolverPost>();
builder.Services.AddScoped<PersonaVendedorResolverPut>();

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
app.UseBlazorFrameworkFiles();
app.UseRouting();
app.UseStaticFiles();
app.MapRazorPages();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
