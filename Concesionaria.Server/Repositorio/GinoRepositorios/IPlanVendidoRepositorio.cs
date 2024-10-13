using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.GinoRepositorios
{
    public interface IPlanVendidoRepositorio : IRepositorio<PlanVendido>
    {
        Task<List<PlanVendido>> SelectPlanYAsociados();
        Task<PlanVendido> SelectPlanYAsociadosById(int id);
    }
}