using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Repositorio.GinoRepositorios
{
    public class PersonaRepositorio : Repositorio<Persona>, IPersonaRepositorio
    {
        private readonly Context context;

        public PersonaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> ExisteByDocumento(string numDoc)
        {
            try
            {
				var Existe = await context.Personas.AnyAsync(p => p.NumDoc == numDoc);
				return Existe;
			}
            catch (Exception e)
            {
				Console.WriteLine($"Error al obtener los registros: {e.Message}");

				throw;
            }

        }

		public async Task<Persona> SelectCodWhithTipoDoc(string codigo)
		{
			try
			{
				var persona = await context.Personas.Include(p => p.TipoDocumento).FirstOrDefaultAsync(p => p.Codigo == codigo);
				return persona;
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error al obtener los registros: {e.Message}");
				throw;
			}
		}

		public async Task<Persona> SelectByNumDoc(string numDoc)
        {
            try
            {
                var persona = await context.Personas.Include(p => p.TipoDocumento).FirstOrDefaultAsync(p => p.NumDoc == numDoc);
                return persona;

			}
            catch (Exception e)
            {
				Console.WriteLine($"Error al obtener los registros: {e.Message}");
				throw;
            }
        }

        public async Task<List<Persona>> SelectEntidadTD()
        {
            try
            {
                var personas = await context.Personas.Include(p => p.TipoDocumento).ToListAsync(); // Incluye la entidad TipoDocumento
                return personas;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
        }

        public async Task<Persona> SelectEntidadTDById(int id)
        {
            try
            {
                var persona = await context.Personas.Include(p => p.TipoDocumento).FirstOrDefaultAsync(p => p.Id == id);
                return persona;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
        }
    }
}
