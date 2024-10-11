using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.FacundoRepositorios
{
    public interface IVehiculoRepositorio : IRepositorio<Vehiculo>
    {
        Task<Vehiculo?> SelectByIdAsync(int id);
    }
}