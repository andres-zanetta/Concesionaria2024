using Concesionaria.DB.Data.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Repositorio.AndresRepositorios
{
    public interface IClienteRepositorio : IRepositorio<Cliente>
    {

        Task<List<Cliente>> Select();
        Task<Cliente> SelectByDNI(int numDoc);
        Task<Cliente> SelectById(int id);

    }
}