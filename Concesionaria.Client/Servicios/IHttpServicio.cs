
namespace Concesionaria.Client.Servicios
{
    public interface IHttpServicio
    {
        Task<HttpRespuesta<O>> Get<O>(string url);
<<<<<<< HEAD
        Task<HttpRespuesta<O>> Put<O>(string url);

=======

        Task<HttpRespuesta<object>> Post<O>(string url, O entidad);

        Task<HttpRespuesta<object>> Put<O>(string url, O entidad);
>>>>>>> bc872b5b6a89240549cf05b0074cca8679a005b8
    }
}