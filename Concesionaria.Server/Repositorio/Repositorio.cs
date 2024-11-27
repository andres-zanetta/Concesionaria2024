using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Repositorio
{
    public class Repositorio<E> : IRepositorio<E> where E : class, IEntityBase
    {
        private readonly Context context;

        public Repositorio(Context context)
        {
            this.context = context;
        }

        public async Task<bool> Existe(int id)
        {
            var existe = await context.Set<E>().AnyAsync(x => x.Id == id);
            return existe;
        }

        public async Task<List<E>> Select()
        {
            try
            {
                return await context.Set<E>().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw; // Relanzar la excepcion para que se maneje mas arriba
            }
        }

		public async Task<E?> SelectByCod(string cod)
		{
            try
            {
				E EntidadByCodigo = await context.Set<E>().FirstOrDefaultAsync(e => e.Codigo == cod);
				return EntidadByCodigo;
			}
            catch (Exception ex)
            {
				Console.WriteLine($"Error al obtener los registros: {ex.Message}");
				throw;
            }

		}

		public async Task<E> SelectById(int id)
        {
            E? sel = await context.Set<E>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return sel;
        }

        public async Task<int> Insert(E entidad)
        {
            try
            {
                await context.Set<E>().AddAsync(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }

            catch (Exception err)
            {
                throw err;
            }
        }

		public async Task<string> InsertDevuelveCodigo(E entidad)
		{
			try
			{
				await context.Set<E>().AddAsync(entidad);
				await context.SaveChangesAsync();
				return entidad.Codigo;
			}

			catch (Exception err)
			{
				throw err;
			}
		}


		public async Task<bool> Update(int id, E entidad)
        {
            if (id != entidad.Id)
            {
                return false;
            }

            var EntidadExistente = await context.Set<E>().FirstOrDefaultAsync(x => x.Id == id); ; 
            // <- no uso SelectByID por que necesito que trackee la entidad y ese metodo no la trackea.

            if (EntidadExistente == null)
            {
                return false;
            }

            try
            {
                context.Entry(EntidadExistente).CurrentValues.SetValues(entidad);
                //El metodo de arriba toma los valores de la entidad seleccionada por id (EntidadExistente)
                //y los actualiza con los de la entidad pasada como argumento (entidad).

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<bool> Delete(int id)
        {
            var sel = await SelectById(id);

            if (sel == null)
            {
                return false;
            }

            context.Set<E>().Remove(sel);
            await context.SaveChangesAsync();
            return true;
        }      
    }
}
