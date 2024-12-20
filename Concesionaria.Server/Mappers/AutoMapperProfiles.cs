﻿using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Resolvers.AdjudicacionResolver;
using Concesionaria.Server.Resolvers.ClienteResolver;
using Concesionaria.Server.Resolvers.CuotaResolver;
using Concesionaria.Server.Resolvers.PagoResolver;
using Concesionaria.Server.Resolvers.PersonaResolvers;
using Concesionaria.Server.Resolvers.PlanVendidoResolver.POST;
using Concesionaria.Server.Resolvers.PlanVendidoResolver.PUT;
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
           .ForMember(dest => dest.ModeloVehiculo, opt => opt.MapFrom(src => src.TipoPlan.Vehiculo.Modelo))
           .ForMember(dest => dest.MarcaVehiculo, opt => opt.MapFrom(src => src.TipoPlan.Vehiculo.Marca))
           .ForMember(dest => dest.CantidadCuotas, opt => opt.MapFrom(src => src.TipoPlan.CantCuotas))
           .ForMember(dest => dest.AutoEntregado, opt => opt.MapFrom(src => src.Adjudicacion != null ? src.Adjudicacion.AutoEntregado : false))
           .ForMember(dest => dest.PatenteVehiculo, opt => opt.MapFrom(src => src.Adjudicacion != null ? src.Adjudicacion.PatenteVehiculo : null)); 

            // El metodo de arriba agregar al DTO info util trackeada desde persona asociada a un CL o V e info util del TipoPlan y adjudic.

            CreateMap<POST_PlanVendidoDNI_DTO, PlanVendido>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"{src.TipoPlanCodigo}-{src.ClienteDNI}"))
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom<ClienteResolverPost>())
                .ForMember(dest => dest.VendedorId, opt => opt.MapFrom<VendedorResolverPost>())
                .ForMember(dest => dest.TipoPlanId, opt => opt.MapFrom<TipoPlanResolverPlanVendidoPost>())
                .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => DateTime.Today))
                .ForMember(dest => dest.FechaSuscripcion, opt => opt.MapFrom<ClienteFechaSuscripcionPost>());


            CreateMap<PUT_PlanVendidoDNI_DTO, PlanVendido>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"{src.TipoPlanCodigo}-{src.ClienteDNI}"))
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom<ClienteResolverPut>())
                .ForMember(dest => dest.VendedorId, opt => opt.MapFrom<VendedorResolverPut>())
                .ForMember(dest => dest.TipoPlanId, opt => opt.MapFrom<TipoPlanResolverPlanVendidoPut>());

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
                .ForMember(dest => dest.VehiculoCodigo, opt => opt.MapFrom(src => src.PlanVendido.TipoPlan.Vehiculo.Codigo)); 

            // POST 

            CreateMap<POST_AdjudicacionDTO, Adjudicacion>()
                .ForMember(dest => dest.Codigo,opt => opt.MapFrom(src => $"Patente-{src.PatenteVehiculo}"))
                .ForMember(dest => dest.PlanVendidoId, opt => opt.MapFrom<PlanVendidoAdjudicResolverPost>())
                .ForMember(dest => dest.FechaAdjudicacion, opt => opt.MapFrom(src => DateTime.Now)); 

            // PUT

            CreateMap<PUT_AdjudicacionDTO, Adjudicacion>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"Patente-{src.PatenteVehiculo}"))
                .ForMember(dest => dest.PlanVendidoId, opt => opt.MapFrom<PlanVendidoAdjudicResolverPut>()); 


            // Mapeado Tipo Plan ==================================================================

            CreateMap<TipoPlan, GET_TipoPlanDTO>().ForMember(dest => dest.CodigoVehiculo, opt => opt.MapFrom(src => src.Vehiculo.Codigo));

            CreateMap<POST_TipoPlanDTO, TipoPlan>().ForMember(dest => dest.VehiculoId, opt => opt.MapFrom<TipoPlanResolverPost>())
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"{src.NombrePlan}-{src.ValorTotal}"));

            CreateMap<PUT_TipoPlanDTO, TipoPlan>().ForMember(dest => dest.VehiculoId, opt => opt.MapFrom<TipoPlanResolverPut>())
				.ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"{src.NombrePlan}-{src.ValorTotal}")); ;


            // Mapeado Cuota ======================================================================

            CreateMap<Cuota, GET_CuotaDTO>()
                .ForMember(dest => dest.CodigoPlanVendido, opt => opt.MapFrom(src => src.PlanVendido.Codigo))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"{src.FechaInicio:dd-MM-yyyy} - Cuota {src.NumeroCuota}")); ;

            CreateMap<POST_CuotaDTO, Cuota>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"{src.FechaInicio.ToString("dd-MM-yyyy")} - Cuota {src.NumeroCuota}"))
                .ForMember(dest => dest.PlanVendidoId, opt => opt.MapFrom<PlanVendidoCuotaResolverPost>());

            CreateMap<PUT_CuotaDTO, Cuota>()
                .ForMember(dest => dest.PlanVendidoId, opt => opt.MapFrom<PlanVendidoCuotaResolverPut>());

            // Mapeado Pago =======================================================================

            CreateMap<Pago, GET_PagoDTO>()
                .ForMember(dest => dest.CuotaCodigo, opt => opt.MapFrom(src => src.Cuota.Codigo));


            CreateMap<POST_PagoDTO, Pago>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"{src.ReferenciaPago}-{src.CodigoCuota}"))
                .ForMember(dest => dest.CuotaId, opt => opt.MapFrom<PagoCuotaResolverPost>());


            CreateMap<PUT_PagoDTO, Pago>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => $"{src.ReferenciaPago}-{src.CodigoCuota}"))
                .ForMember(dest => dest.CuotaId, opt => opt.MapFrom<PagoCuotaResolverPut>());
        }
    }
}