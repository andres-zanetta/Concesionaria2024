using Concesionaria.DB.Data.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Repositorio.AndresRepositorios
{
    public interface IClienteRepositorio : IRepositorio<Cliente>
    {

        Task<List<Cliente>> Select();
		Task<Cliente> SelectByCodConPersona(string codigo);
		Task<Cliente> SelectByDNI(string numDoc);
        Task<Cliente> SelectById(int id);
		Task<List<Cliente>> SelectConPersona();
	}
}