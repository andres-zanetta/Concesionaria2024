using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio
{
    public interface IAdjudicacionRepositorio : IRepositorio<Adjudicacion>
    {
        Task<List<Adjudicacion>> GetByVehiculoIdAsync(int vehiculoId);
    }
}
