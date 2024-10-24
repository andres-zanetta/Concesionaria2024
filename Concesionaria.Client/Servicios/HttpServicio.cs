﻿
using System.Text.Json;

namespace Concesionaria.Client.Servicios
{
    public class HttpServicio : IHttpServicio
    {
        private readonly HttpClient http;

        public HttpServicio(HttpClient http)
        {
            this.http = http;
        }

        public async Task<HttpRespuesta<O>> Get<O>(string url)
        {
            var response = await http.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerealizar<O>(response);
                return new HttpRespuesta<O>(respuesta, false, response);
            }
            else
            {
                return new HttpRespuesta<O>(default, true, response);
            }
        }

        private async Task<O> DesSerealizar<O>(HttpResponseMessage response)
        {
            var respuesta = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<O>(respuesta, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
