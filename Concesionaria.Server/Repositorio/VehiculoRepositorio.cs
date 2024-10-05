using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using Concesionaria.DB.Data;

namespace Concesionaria.Server.Repositorio
{
    public class VehiculoRepositorio : Repositorio<Vehiculo>, IVehiculoRepositorio
    {
        private readonly Context context;

        public VehiculoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Vehiculo?> SelectById(int id)
        {
            return await context.Set<Vehiculo>().FindAsync(id);
        }

        public async Task<List<Vehiculo>> Select()
        {
            return await context.Set<Vehiculo>().ToListAsync();
        }

        public async Task<int> Insert(Vehiculo entidad)
        {
            await context.Set<Vehiculo>().AddAsync(entidad);
            await context.SaveChangesAsync();
            return entidad.Id;
        }

        public async Task<bool> Update(int id, Vehiculo entidad)
        {
            if (id != entidad.Id)
            {
                return false;
            }

            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entidad = await context.Set<Vehiculo>().FindAsync(id);
            if (entidad == null)
            {
                return false;
            }

            context.Set<Vehiculo>().Remove(entidad);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Existe(int id)
        {
            return await context.Set<Vehiculo>().AnyAsync(e => e.Id == id);
        }
    }
}
