using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio
{
    public interface IRepositorio<E> where E : class, IEntityBase
    {
        object Vehiculos { get; }

        Task AddAsync(Vehiculo vehiculo);
        Task<bool> Delete(int id);
        Task<bool> Existe(int id);
        Task<int> Insert(E entidad);
        Task RemoveAsync(object vehiculo);
        Task SaveChangesAsync();
        Task<List<E>> Select();
        Task<E> SelectById(int id);
        Task<Vendedor> SelectByPersona(id personaId);
        Task<bool> Update(int id, E entidad);
        Task UpdateAsync(Vehiculo vehiculo);
    }
}