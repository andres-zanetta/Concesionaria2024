using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Repositorio.BrunoRepositorios
{
    public class CuotaRepositorio : Repositorio<Cuota>, ICuotaRepositorio
    {
        private readonly Context context;

        public CuotaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        // Muestra una cuota por ID con su plan vendido
        public async Task<Cuota> SelectCuotaConPlanVendidoXId(int id)
        {
            try
            {
                var cuota = await context.Cuotas.Include(c => c.PlanVendido)
                                                .FirstOrDefaultAsync(c => c.Id == id);
                return cuota;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener la cuota: {e.Message}");
                throw;
            }
        }

        // Muestra todas las cuotas vencidas
        public async Task<List<Cuota>> SelectCuotasVencidas()
        {
            try
            {
                var cuotas = await context.Cuotas.Where(c => c.CuotaVencida).ToListAsync();
                return cuotas;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener las cuotas vencidas: {e.Message}");
                throw;
            }
        }
        public async Task<int> Insert(Cuota entidad)
        {
            try
            {
                await context.Set<Cuota>().AddAsync(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }

            catch (Exception err)
            {
                throw err;
            }
        }

    }
}

