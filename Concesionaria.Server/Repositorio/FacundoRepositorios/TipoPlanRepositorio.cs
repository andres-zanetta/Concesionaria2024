using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Repositorio.FacundoRepositorios
{
    public class TipoPlanRepositorio : Repositorio<TipoPlan>, ITipoPlanRepositorio
    {
        private readonly Context context;

        public TipoPlanRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<TipoPlan>> SelectWithVehiculo()
        {
            try
            {
                var tipoPlanes = await context.TipoPlanes.Include(tp => tp.Vehiculo).ToListAsync(); // Incluye la entidad TipoDocumento
				return tipoPlanes;
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error al obtener los registros: {e.Message}");
				throw;
			}
		}

        public async Task<TipoPlan> SelectCodWhithVehiculo(string codigo)
        {
            try
            {
				var tipoPlan = await context.TipoPlanes.Include(tp => tp.Vehiculo).FirstOrDefaultAsync(tp => tp.Codigo == codigo);
				return tipoPlan;
			}
            catch (Exception e)
            {
				Console.WriteLine($"Error al obtener los registros: {e.Message}");
				throw;
			}
		}

        public async Task<TipoPlan> SelectByNombre(string nombre)
        {
            try
            {
                var tipoPlan = await context.TipoPlanes.Include(TP => TP.Vehiculo).FirstOrDefaultAsync(TP => TP.NombrePlan == nombre);
                return tipoPlan;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al traer el registro:  {e.Message}");
                throw e;
            }

        }

        public Task<TipoPlan?> SelectByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
