using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio
{
    public class TipoPlanRepositorio : Repositorio<TipoPlan>
    {
        private readonly Context context;

        public TipoPlanRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
    }
}
