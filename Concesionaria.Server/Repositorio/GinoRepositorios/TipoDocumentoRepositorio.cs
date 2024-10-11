using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Repositorio.GinoRepositorios
{
    public class TipoDocumentoRepositorio : Repositorio<TipoDocumento>, ITipoDocumentoRepositorio
    {
        private readonly Context context;

        public TipoDocumentoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<TipoDocumento?> SelectByCod(string cod)
        {
            TipoDocumento? tipoDoc = await context.TipoDocumentos.FirstOrDefaultAsync(x => x.Codigo == cod);
            return tipoDoc;
        }
        // Agregar metodos especificos con DTO's.
    }
}
