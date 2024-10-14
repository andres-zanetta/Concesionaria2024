using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.BrunoRepositorios
{
    public interface ICuotaRepositorio : IRepositorio<Cuota>
    {
        Task<Cuota> SelectCuotaConPlanVendidoXId(int id);

        Task<List<Cuota>> SelectCuotasVencidas();

    } 
}
