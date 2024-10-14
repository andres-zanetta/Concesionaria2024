using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.BrunoRepositorios
{
    public interface IPagoRepositorio : IRepositorio<Pago>
    {

        Task<Pago> SelectPagoConCuotaId(int id);

        Task<List<Pago>> SelectPagosXCuotaId(int cuotaId);

    }
}
