using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.GinoRepositorios
{
    public interface IPersonaRepositorio : IRepositorio<Persona>
    {
        Task<List<Persona>> SelectEntidadTD();
    }
}