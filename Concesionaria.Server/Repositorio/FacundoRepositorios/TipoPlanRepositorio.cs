using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.FacundoRepositorios
{
    public class TipoPlanRepositorio : Repositorio<TipoPlan>, ITipoPlanRepositorio
    {
        private readonly Context context;

        public TipoPlanRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public Task<TipoPlan?> SelectByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
