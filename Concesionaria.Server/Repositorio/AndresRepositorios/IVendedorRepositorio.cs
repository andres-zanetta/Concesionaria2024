using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.AndresRepositorios
{
    public interface IVendedorRepositorio : IRepositorio<Vendedor>
    {
        Task<Vendedor> SelectByPersona(int personaId);

      
    }
}
