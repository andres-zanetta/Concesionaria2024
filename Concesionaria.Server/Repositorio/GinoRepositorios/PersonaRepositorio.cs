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

        public async Task<List<Persona>> SelectEntidadTD()
        {
            var personas = await context.Personas.Include(p => p.TipoDocumento).ToListAsync(); // Incluye la entidad TipoDocumento
            return personas;
        }
    }
}
