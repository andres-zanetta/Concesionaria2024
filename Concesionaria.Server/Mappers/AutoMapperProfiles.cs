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
        public AutoMapperProfiles()
        {
            CreateMap<CrearPersonaDTO, Persona>();
            CreateMap<CrearPlanVendidoDTO, PlanVendido>();
            CreateMap<CrearTipoDocumentoDTO, TipoDocumento>();
            CreateMap<ActualizarTipoDocumentoDTO, TipoDocumento>();
            CreateMap<CrearClienteDTO,Cliente> ();
            CreateMap<CrearVendedorDTO, Vendedor>();
            CreateMap<CrearVehiculoDTO, Vehiculo>();
            CreateMap<CrearAdjudicacionDTO, Adjudicacion>();
            CreateMap<CrearTipoPlanDTO, TipoPlan>();
            CreateMap<CrearCuotaDTO, Cuota>();
            CreateMap<CrearPagoDTO, Pago>();
        }
    }
}
