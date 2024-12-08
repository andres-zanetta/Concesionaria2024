using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio.AndresRepositorios;
using Concesionaria2024.Shared.DTO.GinoDTO.PlanVendido;

namespace Concesionaria.Server.Resolvers.PlanVendidoResolver.POST
{
    public class ClienteFechaSuscripcionPost : IValueResolver<POST_PlanVendidoDNI_DTO, PlanVendido, DateTime>
    {
        private readonly IClienteRepositorio clienteRepositorio;

        public ClienteFechaSuscripcionPost(IClienteRepositorio clienteRepositorio)
        {
            this.clienteRepositorio = clienteRepositorio;
        }

        public DateTime Resolve(POST_PlanVendidoDNI_DTO source, PlanVendido destination, DateTime DestMember, ResolutionContext context)
        {
            var cliente = clienteRepositorio.SelectByDNI(source.ClienteDNI).Result;
            if (cliente == null)
            {
                throw new KeyNotFoundException($"No se encontró un Cliente con el Número de Documento: {source.ClienteDNI}");
            }

            return cliente.FechaInicio;
        }
    }
}
