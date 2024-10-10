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

        public async Task<Vehiculo?> SelectByIdAsync(int id)
        {
            try
            {
                return await context.Vehiculos.FirstOrDefaultAsync(v => v.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el vehículo por ID", ex);
            }
        }
    }
}
