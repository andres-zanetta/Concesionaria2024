using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using Concesionaria.DB.Data;

namespace Concesionaria.Server.Repositorio
{
    public class ClienteRepositorio: Repositorio<Cliente>,IClienteRepositorio
    {
        private readonly Context context;

        public ClienteRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<bool> Existe(int id)
        {
            return await context.Set<Cliente>().AnyAsync(e => e.Id == id);
        }
        public async Task<Cliente?> SelectById(int id)
        {
            Cliente? cliente= await context.Clientes.FirstOrDefaultAsync(x => x.PersonaId == id);
            return cliente;
        }

        public async Task<List<Cliente>> Select()
        {
            return await context.Set<Cliente>().ToListAsync();
        }

        public async Task<int> Insert(Cliente entidad)
        {
            await context.Set<Cliente>().AddAsync(entidad);
            await context.SaveChangesAsync();
            return entidad.Id;
        }

        public async Task<bool> Update(int id, Cliente entidad)
        {
            if (id != entidad.Id)
            {
                return false;
            }

            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entidad = await context.Set<Cliente>().FindAsync(id);
            if (entidad == null)
            {
                return false;
            }

            context.Set<Cliente>().Remove(entidad);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
