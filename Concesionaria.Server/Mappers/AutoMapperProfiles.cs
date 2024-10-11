using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria2024.Shared.DTO.AndresDTO;
using Concesionaria2024.Shared.DTO.BrunoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO;
using Concesionaria2024.Shared.DTO.GinoDTO;

namespace Concesionaria.Server.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        //                                       CreateMap<Origen, Destino>     <--- Formato
        public AutoMapperProfiles()
        {
            // Mapeado Persona ====================================================================

            CreateMap<Persona, GET_PersonaDTO>().ForMember(dest => dest.TipoDocumentoNombre, opt => opt.MapFrom(src => src.TipoDocumento.Nombre));
            // Mapea el nombre del TipoDocumento a TipoDocumentoNombre en Persona
            CreateMap<POST_PersonaDTO, Persona>();
            CreateMap<PUT_PersonaDTO, Persona>();

            // Mapeado Plan Vendido ===============================================================

            CreateMap<POST_PlanVendidoDTO, PlanVendido>();

            // Mapeado Tipo Documento =============================================================

            CreateMap<TipoDocumento, GET_TipoDocumentoDTO>();
            CreateMap<GET_TipoDocumentoDTO, TipoDocumento>();
            CreateMap<POST_TipoDocumentoDTO, TipoDocumento>();
            CreateMap<PUT_TipoDocumentoDTO, TipoDocumento>();

            // Mapeado Cliente ====================================================================

            CreateMap<CrearClienteDTO,Cliente> ();

            // Mapeado Vendedor ===================================================================

            CreateMap<CrearVendedorDTO, Vendedor>();

            // Mapeado Vehiculo ===================================================================

            CreateMap<CrearVehiculoDTO, Vehiculo>();

            // Mapeado Adjudicacion ===============================================================

            CreateMap<CrearAdjudicacionDTO, Adjudicacion>();

            // Mapeado Tipo Plan ==================================================================

            CreateMap<CrearTipoPlanDTO, TipoPlan>();

            // Mapeado Cuota ======================================================================

            CreateMap<CrearCuotaDTO, Cuota>();

            // Mapeado Pago =======================================================================

            CreateMap<CrearPagoDTO, Pago>();
        }
    }
}
