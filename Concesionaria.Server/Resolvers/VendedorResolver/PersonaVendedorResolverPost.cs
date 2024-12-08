﻿using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.AndresDTO;

namespace Concesionaria.Server.Resolvers.VendedorResolver
{
	public class PersonaVendedorResolverPost : IValueResolver<POST_VendedorDTO, Vendedor, int>
	{
		private readonly IPersonaRepositorio personaRepositorio;

		public PersonaVendedorResolverPost(IPersonaRepositorio personaRepositorio)
		{
			this.personaRepositorio = personaRepositorio;
		}

		public int Resolve(POST_VendedorDTO source, Vendedor destination, int destMember, ResolutionContext context)
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