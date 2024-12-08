using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.GinoDTO.Persona;

namespace Concesionaria.Server.Resolvers.PersonaResolvers
{
    public class TipoDocumentoResolverPut : IValueResolver<PUT_PersonaNumDocDTO, Persona, int>
    {
        private readonly IRepositorio<TipoDocumento> tipoDocRepo;

        public TipoDocumentoResolverPut(IRepositorio<TipoDocumento> tipoDocRepo)
        {
            this.tipoDocRepo = tipoDocRepo;
        }

        public int Resolve(PUT_PersonaNumDocDTO origen, Persona destino, int destMember, ResolutionContext context)
        {
            var tipoDoc = tipoDocRepo.SelectByCod(origen.DocumentoCodigo).Result;
            if (tipoDoc == null)
            {
                throw new KeyNotFoundException($"No se encontró un Tipo de Documento con el código: {origen.DocumentoCodigo}");
            }

            return tipoDoc.Id;
        }
    }
}
