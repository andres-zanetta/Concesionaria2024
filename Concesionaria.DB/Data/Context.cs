using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria.DB.Data
{
    public class Context : DbContext
    {
        public DbSet<Adjudicacion> Adjudicaciones {  get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuota> Cuotaas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<PlanVendido> PlanesVendidos { get; set; }
        public DbSet<TipoPlan> TipoPlanes { get; set; }
        public DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Vendedor> Vendedors { get; set; }

        // Consultar: ¿Corresponde agregar una tabla "Direccion" donde guardar calle, barrio, loc, prov?

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var cascadeFKs = modelBuilder.Model.G­etEntityTypes()
                                          .SelectMany(t => t.GetForeignKeys())
                                          .Where(fk => !fk.IsOwnership
                                                       && fk.DeleteBehavior == DeleteBehavior.Casca­de);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restr­ict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
