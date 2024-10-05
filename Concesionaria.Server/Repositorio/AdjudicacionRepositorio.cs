using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio
{
    public class AdjudicacionRepositorio : Repositorio<Adjudicacion>
    {
        private readonly Context context;

        public AdjudicacionRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
    }
}
