
namespace Concesionaria.Client.Servicios
{
    public interface IHttpServicio
    {
        Task<HttpRespuesta<O>> Get<O>(string url);
        Task<HttpRespuesta<O>> Put<O>(string url);

    }
}