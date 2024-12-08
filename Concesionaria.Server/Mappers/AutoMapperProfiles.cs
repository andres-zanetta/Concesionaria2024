using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Resolvers.ClienteResolver;
using Concesionaria.Server.Resolvers.PersonaResolvers;
using Concesionaria.Server.Resolvers.TipoPlanResolvers;
using Concesionaria.Server.Resolvers.VendedorResolver;
using Concesionaria2024.Shared.DTO.AndresDTO;
using Concesionaria2024.Shared.DTO.BrunoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Adjudicacion;
using Concesionaria2024.Shared.DTO.FacundoDTO.Concesionaria2024.Shared.DTO.FacundoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.TipoPlan;
using Concesionaria2024.Shared.DTO.FacundoDTO.Vehiculo;
using Concesionaria2024.Shared.DTO.GinoDTO.Persona;
using Concesionaria2024.Shared.DTO.GinoDTO.PlanVendido;
using Concesionaria2024.Shared.DTO.GinoDTO.TIpoDocumento;

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

            CreateMap<POST_PersonaNumDocDTO, POST_PersonaDTO>().ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"{src.NumDoc}-{src.DocumentoCodigo}"))
                .ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom<TipoDocumentoResolverPost>());
            CreateMap<POST_PersonaDTO, Persona>();

            CreateMap<PUT_PersonaNumDocDTO, Persona>().ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom<TipoDocumentoResolverPut>())
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"{src.NumDoc}-{src.DocumentoCodigo}"));


            // Mapeado Plan Vendido ===============================================================

            CreateMap<PlanVendido, GET_PlanVendidoDTO>()
           .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => $"{src.Cliente.Persona.Nombre} {src.Cliente.Persona.Apellido}"))
           .ForMember(dest => dest.NombreVendedor, opt => opt.MapFrom(src => $"{src.Vendedor.Persona.Nombre} {src.Vendedor.Persona.Apellido}"))
           .ForMember(dest => dest.NombrePlan, opt => opt.MapFrom(src => $"{src.TipoPlan.NombrePlan}"))
           .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => $"{src.TipoPlan.ValorTotal}"))
           .ForMember(dest => dest.AutoEntregado, opt => opt.MapFrom(src => src.Adjudicacion != null ? src.Adjudicacion.AutoEntregado : false))
           .ForMember(dest => dest.PatenteVehiculo, opt => opt.MapFrom(src => src.Adjudicacion != null ? src.Adjudicacion.PatenteVehiculo : null))
           .ForMember(dest => dest.ModeloVehiculo, opt => opt.MapFrom(src => src.Adjudicacion != null ? src.Adjudicacion.Vehiculo.Modelo : null))
           .ForMember(dest => dest.MarcaVehiculo, opt => opt.MapFrom(src => src.Adjudicacion != null ? src.Adjudicacion.Vehiculo.Marca : null)); 

            // El metodo de arriba agregar al DTO info util trackeada desde persona asociada a un CL o V e info util del TipoPlan y adjudic.

            CreateMap<POST_PlanVendidoDTO, PlanVendido>();
            CreateMap<PUT_PlanVendidoDTO, PlanVendido>();

            // Mapeado Tipo Documento =============================================================

            CreateMap<TipoDocumento, GET_TipoDocumentoDTO>();
            CreateMap<POST_TipoDocumentoDTO, TipoDocumento>();
            CreateMap<PUT_TipoDocumentoDTO, TipoDocumento>();

            // Mapeado Cliente ====================================================================

            CreateMap<Cliente, GET_ClienteDTO>()
           .ForMember(dest => dest.NombrePersona, opt => opt.MapFrom(src => $"{src.Persona.Nombre}-{src.Persona.Apellido}"))
           .ForMember(dest => dest.NumDoc, opt => opt.MapFrom(src => src.Persona.NumDoc));

            CreateMap<POST_ClienteConNumDocDTO, Cliente>()
                .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.PersonaId, opt => opt.MapFrom<PersonaResolverPost>())
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"Cliente-{src.NumDoc}"));


            CreateMap<PUT_ClienteDTO, Cliente>()
                .ForMember(dest => dest.PersonaId, opt => opt.MapFrom<PersonaResolverPut>())
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"Cliente-{src.NumDoc}"));


            // Mapeado Vendedor ===================================================================


            CreateMap<Vendedor, GET_VendedorDTO>()
		        .ForMember(dest => dest.NombrePersona, opt => opt.MapFrom(src => $"{src.Persona.Nombre}-{src.Persona.Apellido}"))
		        .ForMember(dest => dest.NumDoc, opt => opt.MapFrom(src => src.Persona.NumDoc))
                .ForMember(dest => dest.CantPlanesVendidos, opt => opt.MapFrom(src => src.planVendidos.Count));


			CreateMap<POST_VendedorDTO, Vendedor>()
				.ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => DateTime.Now))
				.ForMember(dest => dest.PersonaId, opt => opt.MapFrom<PersonaVendedorResolverPost>())
				.ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"Vendedor-{src.NumDoc}"));

			CreateMap<PUT_VendedorDTO, Vendedor>()
	            .ForMember(dest => dest.PersonaId, opt => opt.MapFrom<PersonaVendedorResolverPut>())
	            .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"Vendedor-{src.NumDoc}"));


			// Mapeado Vehiculo ===================================================================

			CreateMap<Vehiculo, GET_VehiculoDTO>();              


            // Asigno el valor de marca,modelo y motor al codigo, para dejar de depender del id y trabajar con algo cercano al al lenguaje del cliente
            CreateMap<POST_VehiculoDTO, Vehiculo>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"{src.Marca}-{src.Modelo}-{src.Motor}"));


            CreateMap<PUT_VehiculoDTO, Vehiculo>()
				.ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"{src.Marca}-{src.Modelo}-{src.Motor}"));


			// Mapeado Adjudicacion ===============================================================

			CreateMap<Adjudicacion, GET_AdjudicacionDTO>()
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.FechaAdjudicacion)) 
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.AutoEntregado ? "Entregado" : "Pendiente")) 
                .ForMember(dest => dest.PatenteVehiculo, opt => opt.MapFrom(src => src.PatenteVehiculo)); 

            // POST 

            CreateMap<POST_AdjudicacionDTO, Adjudicacion>()
                .ForMember(dest => dest.FechaAdjudicacion, opt => opt.MapFrom(src => src.Fecha))  
                .ForMember(dest => dest.PatenteVehiculo, opt => opt.MapFrom(src => src.PatenteVehiculo))
                .ForMember(dest => dest.VehiculoId, opt => opt.MapFrom(src => src.VehiculoId)); 

            // PUT

            CreateMap<PUT_AdjudicacionDTO, Adjudicacion>()
                .ForMember(dest => dest.FechaAdjudicacion, opt => opt.MapFrom(src => src.FechaAdjudicacion)) 
                .ForMember(dest => dest.PatenteVehiculo, opt => opt.MapFrom(src => src.PatenteVehiculo)) 
                .ForMember(dest => dest.AutoEntregado, opt => opt.MapFrom(src => src.AutoEntregado)); 


            // Mapeado Tipo Plan ==================================================================

            CreateMap<TipoPlan, GET_TipoPlanDTO>().ForMember(dest => dest.CodigoVehiculo, opt => opt.MapFrom(src => src.Vehiculo.Codigo));

            CreateMap<POST_TipoPlanDTO, TipoPlan>().ForMember(dest => dest.VehiculoId, opt => opt.MapFrom<TipoPlanResolverPost>())
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"{src.NombrePlan}-{src.ValorTotal}"));

            CreateMap<PUT_TipoPlanDTO, TipoPlan>().ForMember(dest => dest.VehiculoId, opt => opt.MapFrom<TipoPlanResolverPut>())
				.ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"{src.NombrePlan}-{src.ValorTotal}")); ;


            // Mapeado Cuota ======================================================================

            CreateMap<CrearCuotaDTO, Cuota>();

            // Mapeado Pago =======================================================================

            CreateMap<CrearPagoDTO, Pago>();
        
        }
    }
}
