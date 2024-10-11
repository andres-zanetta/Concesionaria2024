using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Repositorio.AndresRepositorios
{
    public class VendedorRepositorio : Repositorio<Vendedor>, IVendedorRepositorio
    {
        private readonly Context context;
        public VendedorRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Vendedor?> SelectByPersona(int personaId)
        {
            Vendedor? V = await context.Vendedores.
                        FirstOrDefaultAsync(x => x.PersonaId == personaId);
            return V;
        }




        //public async Task<Vendedor?> SelectByFecha(DateTime fechaInicio)
        //{
        //    Vendedor? V = await context.Vendedores.
        //               FirstOrDefaultAsync(x => x.FechaInicio== fechaInicio);
        //    return V;
        //}




    }

}
