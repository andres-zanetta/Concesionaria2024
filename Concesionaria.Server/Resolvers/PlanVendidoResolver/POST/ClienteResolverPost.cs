using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio.AndresRepositorios;
using Concesionaria2024.Shared.DTO.GinoDTO.PlanVendido;

namespace Concesionaria.Server.Resolvers.PlanVendidoResolver.POST
{
    public class ClienteResolverPost : IValueResolver<POST_PlanVendidoDNI_DTO, PlanVendido, int>
    {
        private readonly IClienteRepositorio clienteRepositorio;

        public ClienteResolverPost(IClienteRepositorio clienteRepositorio)
        {
            this.clienteRepositorio = clienteRepositorio;
        }

        public int Resolve(POST_PlanVendidoDNI_DTO source, PlanVendido destination, int destMember, ResolutionContext context)
        {
            var cliente = clienteRepositorio.SelectByDNI(source.ClienteDNI).Result;
            if (cliente == null)
            {
                throw new KeyNotFoundException($"No se encontró un Cliente con el Número de Documento: {source.ClienteDNI}");
            }

            return cliente.Id;
        }
    }
}
