using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.AndresRepositorios
{
    public interface IClienteRepositorio : IRepositorio<Cliente>
    {
        Task<Cliente?> SelectById(int id);

    }
}