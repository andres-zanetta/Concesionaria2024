using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio
{
    public class PlanVendidoRpositorio : Repositorio<PlanVendido>
    {
        private readonly Context context;

        public PlanVendidoRpositorio(Context context) : base(context)
        {
            this.context = context;
        }
    }
}
