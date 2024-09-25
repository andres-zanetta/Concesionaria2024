using Concesionaria.DB.Data.Entidades;

namespace Concesionaria.Server.Repositorio
{
    public interface ITipoDocumentoRepositorio
    {
        Task<TipoDocumento?> SelectByCod(string cod);
    }
}