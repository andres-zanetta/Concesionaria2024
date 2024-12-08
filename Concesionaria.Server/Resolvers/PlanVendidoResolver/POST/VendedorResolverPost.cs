using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio.AndresRepositorios;
using Concesionaria2024.Shared.DTO.GinoDTO.PlanVendido;

namespace Concesionaria.Server.Resolvers.PlanVendidoResolver.POST
{
    public class VendedorResolverPost : IValueResolver<POST_PlanVendidoDNI_DTO, PlanVendido, int>
    {
        private readonly IVendedorRepositorio vendedorRepositorio;

        public VendedorResolverPost(IVendedorRepositorio vendedorRepositorio)
        {
            this.vendedorRepositorio = vendedorRepositorio;
        }

        public int Resolve(POST_PlanVendidoDNI_DTO source, PlanVendido destination, int destMember, ResolutionContext context)
        {
            var vendedor = vendedorRepositorio.SelectByDNI(source.VendedorDNI).Result;
            if (vendedor == null)
            {
                throw new KeyNotFoundException($"No se encontró un Vendedor con el Número de Documento: {source.VendedorDNI}");
            }

            return vendedor.Id;
        }
    }
}
