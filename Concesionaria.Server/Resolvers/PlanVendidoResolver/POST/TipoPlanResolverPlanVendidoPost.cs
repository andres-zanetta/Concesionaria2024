using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio.FacundoRepositorios;
using Concesionaria2024.Shared.DTO.GinoDTO.PlanVendido;

namespace Concesionaria.Server.Resolvers.PlanVendidoResolver.POST
{
    public class TipoPlanResolverPlanVendidoPost : IValueResolver<POST_PlanVendidoDNI_DTO, PlanVendido, int>
    {
        private readonly ITipoPlanRepositorio tipoPlanRepositorio;

        public TipoPlanResolverPlanVendidoPost(ITipoPlanRepositorio tipoPlanRepositorio)
        {
            this.tipoPlanRepositorio = tipoPlanRepositorio;
        }

        public int Resolve(POST_PlanVendidoDNI_DTO source, PlanVendido destination, int destMember, ResolutionContext context)
        {
            var tipoPlan = tipoPlanRepositorio.SelectCodWhithVehiculo(source.TipoPlanCodigo).Result;
            if (tipoPlan == null)
            {
                throw new KeyNotFoundException($"No se encontró un Tipo de Plan con el Codigo: {source.TipoPlanCodigo}");
            }

            return tipoPlan.Id;
        }
    }
}
