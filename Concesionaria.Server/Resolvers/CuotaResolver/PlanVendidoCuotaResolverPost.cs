﻿using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.BrunoDTO;

namespace Concesionaria.Server.Resolvers.CuotaResolver
{
    public class PlanVendidoCuotaResolverPost : IValueResolver<POST_CuotaDTO, Cuota, int>
    {
        private readonly IPlanVendidoRepositorio planVendidoRepositorio;

        public PlanVendidoCuotaResolverPost(IPlanVendidoRepositorio planVendidoRepositorio)
        {
            this.planVendidoRepositorio = planVendidoRepositorio;
        }

        public int Resolve(POST_CuotaDTO source, Cuota destination, int destMember, ResolutionContext context)
        {
            var persona = planVendidoRepositorio.SelectPlanYAsociadosByCodigo(source.CodigoPlanVendido).Result;
            if (persona == null)
            {
                throw new KeyNotFoundException($"No se encontró una Persona con el código: {source.CodigoPlanVendido}");
            }

            return persona.Id;
        }
    }
}