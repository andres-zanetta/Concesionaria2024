using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.AndresDTO;

namespace Concesionaria.Server.Resolvers.ClienteResolver
{
	public class PersonaResolverPut : IValueResolver<PUT_ClienteDTO, Cliente, int>
	{
		private readonly IPersonaRepositorio personaRepositorio;

		public PersonaResolverPut(IPersonaRepositorio personaRepositorio)
        {
			this.personaRepositorio = personaRepositorio;
		}
        public int Resolve(PUT_ClienteDTO source, Cliente destination, int destMember, ResolutionContext context)
		{
			var persona = personaRepositorio.SelectByNumDoc(source.NumDoc).Result;
			if (persona == null)
			{
				throw new KeyNotFoundException($"No se encontró una Persona con el código: {source.NumDoc}");
			}

			return persona.Id;
		}
	}
}
