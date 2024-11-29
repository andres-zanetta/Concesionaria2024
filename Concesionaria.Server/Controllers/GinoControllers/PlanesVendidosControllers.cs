using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.AndresRepositorios;
using Concesionaria.Server.Repositorio.FacundoRepositorios;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.GinoDTO.Persona;
using Concesionaria2024.Shared.DTO.GinoDTO.PlanVendido;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Controllers.GinoControllers
{
    [ApiController]
    [Route("api/PlanVendido")]
    public class PlanesVendidosControllers : ControllerBase
    {
        private readonly IAdjudicacionRepositorio repoAdjudicacion;
        private readonly ITipoPlanRepositorio repoTipoPlan;
        private readonly IVendedorRepositorio repoVendedor;
        private readonly IClienteRepositorio repoCliente;
        private readonly IPlanVendidoRepositorio repositorio;
        private readonly IMapper mapper;

        public PlanesVendidosControllers( IAdjudicacionRepositorio repoAdjudicacion , ITipoPlanRepositorio repoTipoPlan, IVendedorRepositorio repoVendedor,
                                        IClienteRepositorio repoCliente, IPlanVendidoRepositorio repositorio, IMapper mapper)
        {
            this.repoAdjudicacion = repoAdjudicacion;
            this.repoTipoPlan = repoTipoPlan;
            this.repoVendedor = repoVendedor;
            this.repoCliente = repoCliente;
            this.repositorio = repositorio;
            this.mapper = mapper;
        }
                                  
        [HttpGet]
        public async Task<ActionResult<List<GET_PlanVendidoDTO>>> Get()
        {
            try
            {
                var ListPVendido = await repositorio.SelectPlanYAsociados();
                var ListPVendidoDTO = mapper.Map<List<GET_PlanVendidoDTO>>(ListPVendido);
                ListPVendidoDTO.ForEach(dto => Console.WriteLine($"Id: {dto.Id}"));
                return Ok(ListPVendidoDTO);
            }
            catch (Exception e)
            {
                // Registrar el error para el diagnóstico
                Console.WriteLine($"Error en el método GET: {e.Message}");
                return StatusCode(500, $"Ocurrió un error interno: {e.Message}");
            }
        }

        [HttpGet("GetByCod/{cod}")]
        public async Task<ActionResult<GET_PlanVendidoDTO>> GetByCode(string cod)
        {
            try
            {
                var planVendidoCod = await repositorio.SelectPlanYAsociadosByCodigo(cod);
                if (planVendidoCod == null)
                {
                    return NotFound($"No se encontró el plan {cod}");
                }
                var planVendidoDTO = mapper.Map<GET_PlanVendidoDTO>(planVendidoCod);
                return Ok(planVendidoDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error en el método GET: {e.Message}");
                return StatusCode(500, $"Ocurrió un error interno: {e.Message}");
            }
        }


        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(POST_PlanVendidoDNI_DTO POST_entidadDNI_DTO)
        {
            try
            {
                var cliente = await repoCliente.SelectByDNI(POST_entidadDNI_DTO.ClienteDNI);
                if (cliente == null)
                {
                    return NotFound($"Cliente con DNI {POST_entidadDNI_DTO.ClienteDNI} no encontrado.");
                }

                var vendedor = await repoVendedor.SelectByDNI(POST_entidadDNI_DTO.VendedorDNI);
                if (vendedor == null)
                {
                    return NotFound($"Vendedor con DNI {POST_entidadDNI_DTO.VendedorDNI} no encontrado.");
                }

                var tipoPlan = await repoTipoPlan.SelectByNombre(POST_entidadDNI_DTO.TipoPlanNombre);
                if (tipoPlan == null)
                {
                    return NotFound($"Tipo de plan con nombre {POST_entidadDNI_DTO.TipoPlanNombre} no encontrado.");
                }


                var POST_entidadDTO = new POST_PlanVendidoDTO
                {
                    Codigo = POST_entidadDNI_DTO.Codigo,
                    FechaSuscripcion = cliente.FechaInicio,
                    Descripcion = POST_entidadDNI_DTO.Descripcion,
                    Estado = POST_entidadDNI_DTO.Estado,
                    FechaInicio = DateTime.Today,
                    VendedorId = vendedor.Id,
                    ClienteId = cliente.Id,
                    TipoPlanId = tipoPlan.Id,

                    //Mapear

                };

                PlanVendido entidad = mapper.Map<PlanVendido>(POST_entidadDTO);
                return Ok(await repositorio.Insert(entidad));
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return BadRequest($"Error: {e.Message}. Inner Exception: {e.InnerException.Message}");
                }
                return BadRequest(e.Message);
            }
        }

		[HttpPut("CodigoAModificar/{codigo}")]
		public async Task<ActionResult> Put(string codigo, [FromBody] PUT_PlanVendidoDNI_DTO PUT_EntidadDNI_DTO)
		{
            var cliente = await repoCliente.SelectByDNI(PUT_EntidadDNI_DTO.ClienteDNI);
            if (cliente == null)
            {
                return NotFound($"Cliente con DNI {PUT_EntidadDNI_DTO.ClienteDNI} no encontrado.");
            }

            var vendedor = await repoVendedor.SelectByDNI(PUT_EntidadDNI_DTO.VendedorDNI);
            if (vendedor == null)
            {
                return NotFound($"Vendedor con DNI {PUT_EntidadDNI_DTO.VendedorDNI} no encontrado.");
            }

            var tipoPlan = await repoTipoPlan.SelectByNombre(PUT_EntidadDNI_DTO.TipoPlanNombre);
            if (tipoPlan == null)
            {
                return NotFound($"Tipo de plan con nombre {PUT_EntidadDNI_DTO.TipoPlanNombre} no encontrado.");
            }

            var adjudicacion = await repoAdjudicacion.SelectByCodigo(PUT_EntidadDNI_DTO.AdjudicaciónCodigo);


            var EntidadDTO = new PUT_PlanVendidoDTO
            {
                FechaSuscripcion = cliente.FechaInicio,
                FechaSorteo = PUT_EntidadDNI_DTO.FechaSorteo,
                Descripcion = PUT_EntidadDNI_DTO.Descripcion,
                Estado = PUT_EntidadDNI_DTO.Estado,
                FechaInicio = DateTime.Today,
                FechaFin = PUT_EntidadDNI_DTO.FechaFin,
                PlanEnMora = PUT_EntidadDNI_DTO.PlanEnMora,
                VendedorId = vendedor.Id,
                ClienteId = cliente.Id,
                TipoPlanId = tipoPlan.Id,
                AdjudicacionId = adjudicacion.Id,

                //Mapear

            };

            if (!await repositorio.ExisteByCodigo(codigo))
            {
				return BadRequest($"No se encontró un vehiculo con el código {codigo}, compruebe el valor ingresado.");
			}

            var planVendido = await repositorio.SelectPlanYAsociadosByCodigo(codigo);

            if (planVendido == null)
            {
				return NotFound($"No se encontró una persona con el código {codigo}, compruebe el valor ingresado.");
			}

            mapper.Map(EntidadDTO, planVendido);

            // Log para verificar los valores actualizados en verde 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"PlanVendido actualizado: {planVendido.VendedorId}, {planVendido.ClienteId}, {planVendido.TipoPlanId}, {planVendido.AdjudicacionId}");
            Console.ResetColor();

            try
            {
                await repositorio.Update(planVendido.Id, planVendido);
                var personaDTO = mapper.Map<GET_PlanVendidoDTO>(planVendido);
                return Ok(personaDTO);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return BadRequest($"Error: {e.Message}. Inner Exception: {e.InnerException.Message}");
                }
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El plan {id} no existe");
            }
            PlanVendido EntidadABorrar = new PlanVendido();
            EntidadABorrar.Id = id;

            if (await repositorio.Delete(id))
            {
                return Ok($"El plan {id} fue eliminado");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
