using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using Concesionaria.DB.Data;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Repositorio.AndresRepositorios
{
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        private readonly Context context;

        public ClienteRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Cliente> SelectByDNI(string numDoc)
        {
            try
            {
                var cliente = await context.Clientes.Include(C => C.Persona).FirstOrDefaultAsync(C => C.Persona.NumDoc == numDoc);
                return cliente;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros:  {e.Message}");
                throw e;
            }
        }

        public async Task<Cliente> SelectByCodConPersona(string codigo)
        {
            try
            {
				var cliente = await context.Clientes.Include(C => C.Persona).FirstOrDefaultAsync(C => C.Codigo == codigo);
				return cliente;
			}
            catch (Exception e)
            {
				Console.WriteLine($"Error al obtener los registros:  {e.Message}");
				throw e;
			}
        }

        public async Task<List<Cliente>> SelectConPersona()
        {
            try
            {
				var clientes = await context.Clientes.Include(C => C.Persona).ToListAsync();
				return clientes;
			}
            catch (Exception e)
            {
				Console.WriteLine($"Error al obtener los registros:  {e.Message}");
				throw e;
			}
		}

        public async Task<Cliente> SelectByFechaInicio(DateTime fechaInicio)
        {
            try
            {
                Cliente? cliente = await context.Clientes.
                   FirstOrDefaultAsync(x => x.FechaInicio == fechaInicio);
                return cliente;

            }
            catch (Exception e)
            {

                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
         
        }
    }
}
