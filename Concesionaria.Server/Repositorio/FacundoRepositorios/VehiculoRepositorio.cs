using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using Concesionaria.DB.Data;
using System.Threading.Tasks;

namespace Concesionaria.Server.Repositorio.FacundoRepositorios
{
    public class VehiculoRepositorio : Repositorio<Vehiculo>, IVehiculoRepositorio
    {
        private readonly Context context;

        public VehiculoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Vehiculo> GetByMarca(string marca)
        {
            try
            {
				var vehiculo = await context.Vehiculos.FirstOrDefaultAsync(m => m.Marca == marca);
                return vehiculo;
			}
			catch (Exception e)
            {
				Console.WriteLine($"Error al obtener los registros: {e.Message}");
				throw;
            }
        }
    }
}

