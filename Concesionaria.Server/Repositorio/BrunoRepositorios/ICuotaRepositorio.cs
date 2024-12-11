using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.BrunoRepositorios
{
    public interface ICuotaRepositorio : IRepositorio<Cuota>
    {
        Task<bool> DeleteByPVCod(string codigo);
        Task<bool> ExisteConPVIncluido(string codigo);
        Task Generarcuotas(int planVendidoId);
        Task<List<Cuota>> SelectByPVCodigoAll(string codigo);
        Task<Cuota> SelectCuotaConPlanVendidoXId(int id);

        Task<List<Cuota>> SelectCuotasVencidas();
        Task<List<Cuota>> SelectEntidadAll();
        Task<Cuota> SelectEntidadByCodConPagos(string codigo);
        Task<Cuota> SelectEntidadByNumCuotaConPagos(int numCuota);
    } 
}
