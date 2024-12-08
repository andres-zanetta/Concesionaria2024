using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.GinoDTO.Persona;

namespace Concesionaria.Server.Resolvers.PersonaResolvers
{
    public class TipoDocumentoResolverPost : IValueResolver<POST_PersonaNumDocDTO, POST_PersonaDTO, int>
    {
        private readonly IRepositorio<TipoDocumento> tipoDocRepo;

        public TipoDocumentoResolverPost(IRepositorio<TipoDocumento> tipoDocRepo)
        {
            this.tipoDocRepo = tipoDocRepo;
        }

        public int Resolve(POST_PersonaNumDocDTO origen, POST_PersonaDTO destino, int destMember, ResolutionContext context)
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
