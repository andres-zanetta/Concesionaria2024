using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio
{
    public interface ITipoDocumentoRepositorio : IRepositorio<TipoDocumento>
    {
        Task<TipoDocumento?> SelectByCod(string cod);
    }
}