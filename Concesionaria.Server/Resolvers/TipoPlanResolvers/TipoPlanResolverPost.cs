using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.FacundoDTO.TipoPlan;
using Concesionaria2024.Shared.DTO.GinoDTO.Persona;

namespace Concesionaria.Server.Resolvers.TipoPlanResolvers
{
	public class TipoPlanResolverPost : IValueResolver<POST_TipoPlanDTO, TipoPlan, int>
	{
		private readonly IRepositorio<Vehiculo> vehiculoRepo;

		public TipoPlanResolverPost(IRepositorio<Vehiculo> vehiculoRepo)
		{
			this.vehiculoRepo = vehiculoRepo;
		}

		public int Resolve(POST_TipoPlanDTO origen, TipoPlan destino, int destMember, ResolutionContext context)
		{
			var vehiculo = vehiculoRepo.SelectByCod(origen.CodigoVehiculo).Result;
			if (vehiculo == null)
			{
				throw new KeyNotFoundException($"No se encontró un Vehiculo con el código: {origen.CodigoVehiculo}");
			}

			return vehiculo.Id;
		}
	}
}
