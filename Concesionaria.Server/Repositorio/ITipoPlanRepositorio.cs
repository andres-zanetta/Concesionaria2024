using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio
{
    public interface ITipoPlanRepositorio : IRepositorio<TipoPlan>
    {
        Task<TipoPlan?> SelectByIdAsync(int id);
    }
}