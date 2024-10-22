namespace Concesionaria.Client.Servicios
{
    public class HttpRespuesta<O>
    {
        public HttpRespuesta(O respuesta, bool error, HttpResponseMessage httpResponseMessage)
        {
            Respuesta = respuesta;
            Error = error;
            this.httpResponseMessage = httpResponseMessage;
        }

        public O Respuesta { get; }
        public bool Error { get; }
        public HttpResponseMessage httpResponseMessage { get; set; }

    }
}
