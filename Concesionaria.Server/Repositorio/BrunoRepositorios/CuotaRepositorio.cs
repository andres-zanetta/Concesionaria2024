using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio.BrunoRepositorios
{
    public class CuotaRepositorio : Repositorio<Cuota>
    {
        private readonly Context context;

        public CuotaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
    }
}

