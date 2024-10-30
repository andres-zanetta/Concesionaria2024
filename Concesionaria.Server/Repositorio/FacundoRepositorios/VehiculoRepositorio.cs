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

        public async Task<Vehiculo?> SelectByIdAsync(int id)
        {
            return await context.Vehiculos.FirstOrDefaultAsync(v => v.Id == id);
        }
    }
}

