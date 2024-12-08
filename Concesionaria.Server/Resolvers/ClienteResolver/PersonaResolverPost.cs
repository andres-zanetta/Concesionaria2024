using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.AndresDTO;
using Concesionaria2024.Shared.DTO.GinoDTO.Persona;

namespace Concesionaria.Server.Resolvers.ClienteResolver
{
	public class PersonaResolverPost : IValueResolver<POST_ClienteConNumDocDTO, Cliente, int>
	{
		private readonly IPersonaRepositorio personaRepositorio;

		public PersonaResolverPost(IPersonaRepositorio personaRepositorio)
        {
			this.personaRepositorio = personaRepositorio;
		}

        public int Resolve(POST_ClienteConNumDocDTO source, Cliente destination, int destMember, ResolutionContext context)
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
