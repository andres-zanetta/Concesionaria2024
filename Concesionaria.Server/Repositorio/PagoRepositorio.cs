using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio
{
    public class PagoRepositorio : Repositorio<Pago>
    {
        private readonly Context context;

        public PagoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
    }
}

