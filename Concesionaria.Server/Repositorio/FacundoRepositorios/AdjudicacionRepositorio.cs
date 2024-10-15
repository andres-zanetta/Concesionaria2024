using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Repositorio.FacundoRepositorios
{
    public class AdjudicacionRepositorio : IAdjudicacionRepositorio
    {
        private readonly Context context;

        public AdjudicacionRepositorio(Context context)
        {
            this.context = context;
        }

        public async Task<List<Adjudicacion>> Select()
        {
            return await context.Adjudicaciones.ToListAsync();
        }

        public async Task<Adjudicacion> SelectById(int id)
        {
            return await context.Adjudicaciones.FindAsync(id);
        }

        public async Task<int> Insert(Adjudicacion entidad)
        {
            context.Adjudicaciones.Add(entidad);
            await context.SaveChangesAsync();
            return entidad.Id; 
        }

        public async Task<bool> Update(int id, Adjudicacion entidad)
        {
            if (id != entidad.Id) return false; 

            context.Adjudicaciones.Update(entidad);
            return await context.SaveChangesAsync() > 0; 
        }

        public async Task<bool> Delete(int id)
        {
            var adjudicacion = await SelectById(id);
            if (adjudicacion == null) return false; 

            context.Adjudicaciones.Remove(adjudicacion);
            return await context.SaveChangesAsync() > 0; 
        }

        public async Task<bool> Existe(int id)
        {
            return await context.Adjudicaciones.AnyAsync(a => a.Id == id); 
        }

        public async Task<List<Adjudicacion>> GetByVehiculoIdAsync(int vehiculoId)
        {
            return await context.Adjudicaciones
                .Where(a => a.VehiculoId == vehiculoId)
                .ToListAsync(); 
        }
    }
}
