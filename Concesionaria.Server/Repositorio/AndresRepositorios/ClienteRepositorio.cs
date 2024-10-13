using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using Concesionaria.DB.Data;

namespace Concesionaria.Server.Repositorio.AndresRepositorios
{
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        private readonly Context context;

        public ClienteRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Cliente?> SelectById(int id)
        {
            Cliente? cliente = await context.Clientes.FirstOrDefaultAsync(x => x.PersonaId == id);
            return cliente;
        }

        public async Task<Cliente?> SelectByFechaInicio(DateTime fechaInicio)
        {
            Cliente? cliente = await context.Clientes.
                      FirstOrDefaultAsync(x => x.FechaInicio == fechaInicio);
            return cliente;
        }

        //public async Task<Cliente?>SelectByPlanVendido(int planvendido)
        //{
        //    Cliente? cliente = await context.Clientes.FirstOrDefaultAsync(x => x.PlanVendido == planvendido);
        //}

    }
}
