using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.AndresRepositorios
{
    public interface IVendedorRepositorio : IRepositorio<Vendedor>
    {
        Task<List<Vendedor>> SelectByPersona(int personaId);
        Task<Vendedor> SelectById(int id);
        Task<Vendedor> SelectByDNI(int numDoc);
    }
}
