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

        public async Task<Cliente?> SelectById(int id)
        {
            Cliente? cliente= await context.Clientes.FirstOrDefaultAsync(x => x.VehiculoId == id);
            return cliente;
        }


    }
}
