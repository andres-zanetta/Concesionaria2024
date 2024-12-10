using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio.FacundoRepositorios;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.AndresDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Adjudicacion;

namespace Concesionaria.Server.Resolvers.AdjudicacionResolver
{
    public class PlanVendidoAdjudicResolverPut : IValueResolver<PUT_AdjudicacionDTO, Adjudicacion, int>
    {
        private readonly IPlanVendidoRepositorio planVendidoRepositorio;

        public PlanVendidoAdjudicResolverPut(IPlanVendidoRepositorio planVendidoRepositorio)
        {
            this.planVendidoRepositorio = planVendidoRepositorio;
        }

        public int Resolve(PUT_AdjudicacionDTO source, Adjudicacion destination, int destMember, ResolutionContext context)
        {
            var planVendido = planVendidoRepositorio.SelectByCod(source.PlanVendidoCodigo).Result;
            if (planVendido == null)
            {
                throw new KeyNotFoundException($"No se encontró una Persona con el código: {source.PlanVendidoCodigo}");
            }

            return planVendido.Id;
        }
    }
}
