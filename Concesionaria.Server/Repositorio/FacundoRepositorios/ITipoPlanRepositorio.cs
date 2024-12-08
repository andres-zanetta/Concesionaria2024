using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.FacundoRepositorios
{
    public interface ITipoPlanRepositorio : IRepositorio<TipoPlan>
    {
        Task<TipoPlan?> SelectByIdAsync(int id);
        Task<TipoPlan> SelectByNombre(string nombre);
		Task<TipoPlan> SelectCodWhithVehiculo(string codigo);
		Task<List<TipoPlan>> SelectWithVehiculo();
	}
}