using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.FacundoDTO.TipoPlan;

namespace Concesionaria.Server.Resolvers.TipoPlanResolvers
{
	public class TipoPlanResolverPut : IValueResolver<PUT_TipoPlanDTO, TipoPlan, int>
	{
		private readonly IRepositorio<Vehiculo> repoVehiculo;

		public TipoPlanResolverPut(IRepositorio<Vehiculo> repoVehiculo)
        {
			this.repoVehiculo = repoVehiculo;
		}
        public int Resolve(PUT_TipoPlanDTO origen, TipoPlan destination, int destMember, ResolutionContext context)
		{
			var vehiculo = repoVehiculo.SelectByCod(origen.CodigoVehiculo).Result;
			if (vehiculo == null)
			{
				throw new KeyNotFoundException($"No se encontró un Vehiculo con el código: {origen.CodigoVehiculo}");
			}

			return vehiculo.Id;
		}
	}
}
