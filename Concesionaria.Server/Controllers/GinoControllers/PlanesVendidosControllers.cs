using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.AndresRepositorios;
using Concesionaria.Server.Repositorio.FacundoRepositorios;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.AndresDTO;
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

        // GET:-------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<List<GET_PlanVendidoDTO>>> Get()
        {
            try
            {
                var planesVendidos = await repositorio.SelectPlanYAsociados();
                var ListaPlanes = mapper.Map<List<GET_PlanVendidoDTO>>(planesVendidos);
                return Ok(ListaPlanes);
            }
            catch (Exception ex)
            {
                // Registrar el error para el diagnóstico
                Console.WriteLine($"Error en el método GET: {ex.Message}");
                return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
            }
        }

        [HttpGet("Documento/{documento}")]
        public async Task<ActionResult<GET_PlanVendidoDTO>> Get(string documento)
        {
            try
            {
                Cliente? cliente = await repoCliente.SelectByDNI(documento);
                if (cliente == null)
                {
                    return NotFound($"No se encontro un Cliente con el número de docuemnto: {documento}. Favor verificar");
                }

                var planvendido = await repositorio.SelectByIdCliente(cliente.Id);

                var planvendidoDTO = mapper.Map<GET_PlanVendidoDTO>(planvendido);
                return Ok(planvendidoDTO);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return BadRequest($"Error: {ex.Message}. Inner Exception: {ex.InnerException.Message}");
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Codigo/{codigo}")]
        public async Task<ActionResult<GET_PlanVendidoDTO>> GetByCodigo(string codigo)
        {
            try
            {
                PlanVendido? planVendido = await repositorio.SelectPlanYAsociadosByCodigo(codigo);
                if (planVendido == null)
                {
                    return NotFound($"No se encontro un Cliente con el codigo: {codigo}. Favor verificar");
                }
                var planVendidoDTO = mapper.Map<GET_PlanVendidoDTO>(planVendido);
                return Ok(planVendidoDTO);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return BadRequest($"Error: {ex.Message}. Inner Exception: {ex.InnerException.Message}");
                }
                return BadRequest(ex.Message);
            }
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

                var tipoPlan = await repoTipoPlan.SelectCodWhithVehiculo(POST_entidadDNI_DTO.TipoPlanCodigo);
                if (tipoPlan == null)
                {
                    return NotFound($"Tipo de plan con nombre {POST_entidadDNI_DTO.TipoPlanCodigo} no encontrado.");
                }


                PlanVendido entidad = mapper.Map<PlanVendido>(POST_entidadDNI_DTO);
                await repositorio.Insert(entidad);
                return Ok($"Plan vendido cargado exitosamente: Código {entidad.Codigo}, Vendedor {vendedor.Persona.Nombre} {vendedor.Persona.Apellido}");
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

            if (!await repositorio.ExisteByCodigo(codigo))
            {
                return BadRequest($"No se encontró un vehiculo con el código {codigo}, compruebe el valor ingresado.");
            }


            var planVendido = await repositorio.SelectPlanYAsociadosByCodigo(codigo);
            if (planVendido == null)
            {
                return NotFound($"No se encontró un Plan con el código {codigo}, compruebe el valor ingresado.");
            }

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

            var tipoPlan = await repoTipoPlan.SelectCodWhithVehiculo(PUT_EntidadDNI_DTO.TipoPlanCodigo);
            if (tipoPlan == null)
            {
                return NotFound($"Tipo de plan con Codigo {PUT_EntidadDNI_DTO.TipoPlanCodigo} no encontrado.");
            }

            var adjudicacion = await repoAdjudicacion.SelectByCod(PUT_EntidadDNI_DTO.AdjudicaciónCodigo);



            var entidad = mapper.Map(PUT_EntidadDNI_DTO, planVendido);


            // Log para verificar los valores actualizados en verde 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"PlanVendido actualizado: {entidad.VendedorId}, {entidad.ClienteId}, {entidad.TipoPlanId}, {entidad.AdjudicacionId}");
            Console.ResetColor();

            try
            {
                await repositorio.Update(entidad.Id, entidad);
                var entidadDTO = mapper.Map<GET_PlanVendidoDTO>(entidad);
                return Ok(entidadDTO);
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

        [HttpDelete("EliminarCodigo/{codigo}")]
        public async Task<ActionResult> Delete(string codigo)
        {
            var existe = await repositorio.ExisteByCodigo(codigo);

            if (!existe)
            {
                return NotFound($"El Plan Vendido con código {codigo} no existe");
            }

            var EntidadABorrar = await repositorio.SelectByCod(codigo);

            if (EntidadABorrar == null)
            {
                return NotFound($"No se encontró un Plan Vendido con código {codigo}. Favor verificar");
            }

            if (await repositorio.Delete(EntidadABorrar.Id))
            {
                return Ok($"El Plan Vendido con código {codigo} fue eliminado");
            }
            else
            {
                return BadRequest("No se pudo llevar a cabo la acción");
            }
        }
    }
}
