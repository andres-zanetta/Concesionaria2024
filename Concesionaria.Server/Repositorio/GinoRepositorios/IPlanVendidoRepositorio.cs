using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.GinoRepositorios
{
    public interface IPlanVendidoRepositorio : IRepositorio<PlanVendido>
    {
        Task<PlanVendido> SelectByIdCliente(int id);
        Task<List<PlanVendido>> SelectPlanYAsociados();
        Task<PlanVendido> SelectPlanYAsociadosByCodigo(string codigo);
    }
}