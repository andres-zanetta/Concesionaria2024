using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio.FacundoRepositorios;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.FacundoDTO.Adjudicacion;

namespace Concesionaria.Server.Resolvers.AdjudicacionResolver
{
    public class PlanVendidoAdjudicResolverPost : IValueResolver<POST_AdjudicacionDTO, Adjudicacion, int>
    {
        private readonly IPlanVendidoRepositorio planVendidoRepositorio;

        public PlanVendidoAdjudicResolverPost(IPlanVendidoRepositorio planVendidoRepositorio)
        {
            this.planVendidoRepositorio = planVendidoRepositorio;
        }

        public int Resolve(POST_AdjudicacionDTO source, Adjudicacion destination, int destMember, ResolutionContext context)
        {
            var planVendido = planVendidoRepositorio.SelectByCod(source.PlanVendidoCodigo).Result;
            if (planVendido == null)
            {
                throw new KeyNotFoundException($"No se encontró un Plan Vendido con el código: {source.PlanVendidoCodigo}");
            }

            return planVendido.Id;
        }
    }
}
