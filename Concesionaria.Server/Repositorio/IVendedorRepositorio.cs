using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio
{
    public interface IVendedorRepositorio : IRepositorio<Vendedor>
    {
        Task<Vendedor> SelectById(int personaId);
    }
}
