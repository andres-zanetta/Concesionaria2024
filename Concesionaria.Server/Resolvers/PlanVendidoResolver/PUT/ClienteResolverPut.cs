using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio.AndresRepositorios;
using Concesionaria2024.Shared.DTO.GinoDTO.PlanVendido;

namespace Concesionaria.Server.Resolvers.PlanVendidoResolver.PUT
{
    public class ClienteResolverPut : IValueResolver<PUT_PlanVendidoDNI_DTO, PlanVendido, int>
    {
        private readonly IClienteRepositorio clienteRepositorio;

        public ClienteResolverPut(IClienteRepositorio clienteRepositorio)
        {
            this.clienteRepositorio = clienteRepositorio;
        }

        public int Resolve(PUT_PlanVendidoDNI_DTO source, PlanVendido destination, int DestMember, ResolutionContext context)
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
