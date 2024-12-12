using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.BrunoRepositorios
{
    public interface IPagoRepositorio : IRepositorio<Pago>
    {
        Task<List<Pago>> SelectAllConCuota();
        Task<Pago> SelectByReferenciaPago(string refPAgo);
        Task<Pago> SelectCodigoConCuota(string codigo);
        Task<Pago> SelectPagoConCuotaId(int id);

        Task<List<Pago>> SelectPagosXCuotaId(int cuotaId);

    }
}
