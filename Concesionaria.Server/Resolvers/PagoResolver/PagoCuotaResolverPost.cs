using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio.BrunoRepositorios;
using Concesionaria2024.Shared.DTO.BrunoDTO;

namespace Concesionaria.Server.Resolvers.PagoResolver
{
    public class PagoCuotaResolverPost : IValueResolver<POST_PagoDTO, Pago, int>
    {
        private readonly ICuotaRepositorio cuotaRepositorio;

        public PagoCuotaResolverPost(ICuotaRepositorio cuotaRepositorio)
        {
            this.cuotaRepositorio = cuotaRepositorio;
        }

        public int Resolve(POST_PagoDTO source, Pago destination, int destMember, ResolutionContext context)
        {
            var cuota = cuotaRepositorio.SelectEntidadByCodConPagos(source.CodigoCuota).Result;
            if (cuota == null)
            {
                throw new KeyNotFoundException($"No se encontró una Cuota con el código: {source.CodigoCuota}");
            }

            return cuota.Id;
        }
    }
}
