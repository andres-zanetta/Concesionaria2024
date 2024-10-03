using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Repositorio
{
    public class VendedorRepositorio: Repositorio<Vendedor>, IVendedorRepositorio
    {
        private readonly Context context;
        public VendedorRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

 
        public async Task<Vendedor> SelectByPersona(int personaId)
        {
            Vendedor? pepe = await context.Vendedores.
                        FirstOrDefaultAsync(x => x.PersonaId == personaId);
            return pepe;
        }
    }

}
