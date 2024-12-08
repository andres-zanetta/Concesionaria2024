using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.AndresRepositorios
{
    public interface IVendedorRepositorio : IRepositorio<Vendedor>
    {
        
        Task<Vendedor> SelectById(int id);
        Task<Vendedor> SelectByDNI(string numDoc);
		Task<Vendedor> SelectByCodConPersona(string codigo);
		Task<List<Vendedor>> SelectConPersona();
	}
}
