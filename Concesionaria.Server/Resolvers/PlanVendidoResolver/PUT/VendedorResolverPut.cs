using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio.AndresRepositorios;
using Concesionaria2024.Shared.DTO.GinoDTO.PlanVendido;

namespace Concesionaria.Server.Resolvers.PlanVendidoResolver.PUT
{
    public class VendedorResolverPut : IValueResolver<PUT_PlanVendidoDNI_DTO, PlanVendido, int>
    {
        private readonly IVendedorRepositorio vendedorRepositorio;

        public VendedorResolverPut(IVendedorRepositorio vendedorRepositorio)
        {
            this.vendedorRepositorio = vendedorRepositorio;
        }

        public int Resolve(PUT_PlanVendidoDNI_DTO source, PlanVendido destination, int destMember, ResolutionContext context)
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
