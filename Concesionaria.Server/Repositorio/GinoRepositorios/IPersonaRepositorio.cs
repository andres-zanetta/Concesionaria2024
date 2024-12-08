using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.GinoRepositorios
{
    public interface IPersonaRepositorio : IRepositorio<Persona>
    {
		Task<bool> ExisteByDocumento(string numDoc);
		Task<Persona> SelectByNumDoc(string numDoc);
		Task<Persona> SelectCodWhithTipoDoc(string codigo);
		Task<List<Persona>> SelectEntidadTD();
        Task<Persona> SelectEntidadTDById(int id);
    }
}