using Concesionaria.DB.Data.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Repositorio.AndresRepositorios
{
    public interface IClienteRepositorio : IRepositorio<Cliente>
    {

        Task<List<Cliente>> Select();
        Task<Cliente> SelectById(int id);

    }
}