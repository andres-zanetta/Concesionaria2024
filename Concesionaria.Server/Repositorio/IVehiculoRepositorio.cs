using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio
{
    public interface IVehiculoRepositorio : IRepositorio<Vehiculo>
    {
        Task<Vehiculo?> SelectByIdAsync(int id);
    }
}