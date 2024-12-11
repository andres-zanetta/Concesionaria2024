using AutoMapper;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.BrunoRepositorios;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.BrunoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Concesionaria2024.Shared.DTO.FacundoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Vehiculo;
using Concesionaria2024.Shared.DTO.GinoDTO.Persona;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Controllers.BrunoControllers
{
    [ApiController]
    [Route("api/Cuotas")]
    public class CuotasControllers : ControllerBase
    {
        private readonly IPlanVendidoRepositorio repoPlanVendido;
        private readonly ICuotaRepositorio repositorio;
        private readonly IMapper mapper;

        public CuotasControllers(IPlanVendidoRepositorio repoPlanVendido, ICuotaRepositorio repositorio, IMapper mapper)
        {
            this.repoPlanVendido = repoPlanVendido;
            this.repositorio = repositorio;
            this.mapper = mapper;
        }


        // GET: -----------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<List<GET_CuotaDTO>>> GetAll()
        {
            try
            {
                var ListPersonaSelect = await repositorio.SelectEntidadAll();
                var ListPersona = mapper.Map<List<GET_CuotaDTO>>(ListPersonaSelect);
                return Ok(ListPersona);
            }
            catch (Exception ex)
            {
                // Registrar el error para el diagnóstico
                Console.WriteLine($"Error en el método GET: {ex.Message}");
                return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
            }
        }

        [HttpGet("CodigoPlanVendido/{codigo}")]
        public async Task<ActionResult<List<GET_CuotaDTO>>> GetListByPVCod(string codigo)
        {
            try
            {
                var ListPersonaSelect = await repositorio.SelectByPVCodigoAll(codigo);
                var ListPersona = mapper.Map<List<GET_CuotaDTO>>(ListPersonaSelect);
                return Ok(ListPersona);
            }
            catch (Exception ex)
            {
                // Registrar el error para el diagnóstico
                Console.WriteLine($"Error en el método GET: {ex.Message}");
                return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
            }
        }

        [HttpGet("CodigoCuota/{codigo}")]
        public async Task<ActionResult<GET_CuotaDTO>> GetByCod(string codigo)
        {
            try
            {
                Cuota? persona = await repositorio.SelectEntidadByCodConPagos(codigo);
                if (persona == null)
                {
                    return NotFound($"No se encontro una Cuota con el Codigo: {codigo}. Favor verificar");
                }
                var personaDTO = mapper.Map<GET_CuotaDTO>(persona);
                return Ok(personaDTO);
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

        [HttpGet("CuotaNumero/{numCuota}")]
        public async Task<ActionResult<GET_CuotaDTO>> GetByNumCuota(int numCuota)
        {
            try
            {
                Cuota? persona = await repositorio.SelectEntidadByNumCuotaConPagos(numCuota);
                if (persona == null)
                {
                    return NotFound($"No se encontro una Cuota con el codigo: {numCuota}. Favor verificar");
                }
                var personaDTO = mapper.Map<GET_CuotaDTO>(persona);
                return Ok(personaDTO);
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

        // POST: ----------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<int>> Post(POST_CuotaDTO POST_entidadPVCodDTO)
        {
            try
            {
                if (POST_entidadPVCodDTO == null)
                {
                    return BadRequest("Los datos ingresados son nulos. Favor verificar.");
                }

                var planVendido = await repoPlanVendido.SelectByCod(POST_entidadPVCodDTO.CodigoPlanVendido);
                if (planVendido == null)
                {
                    return NotFound($"No se encontró un Plan Vendido con el codigo: {POST_entidadPVCodDTO.CodigoPlanVendido}. Favor verificar.");
                }

                var cuotaMapeada = mapper.Map<Cuota>(POST_entidadPVCodDTO);

                await repositorio.Insert(cuotaMapeada);
                return Ok($"Cuota cargada correctamente. Codigo: {cuotaMapeada.Codigo}, Número de docuemnto: {cuotaMapeada.NumeroCuota}");

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

		// PUT: -----------------------------------------------------------------------
		[HttpPut("CodigoAModificar/{codigo}")]
		public async Task<ActionResult> Put(string codigo, [FromBody] PUT_CuotaDTO PUT_EntidadPVCod_DTO)
		{
			if (!await repositorio.ExisteByCodigo(codigo))
			{
				return BadRequest($"No se encontró una Cuota con el código {codigo}, compruebe el valor ingresado.");
			}

			var cuota = await repositorio.SelectByCod(codigo);

			if (cuota == null)
			{
				return NotFound($"No se encontró una cuota con el código {codigo}, compruebe el valor ingresado.");
			}

            var planVendido = await repoPlanVendido.SelectPlanYAsociadosByCodigo(PUT_EntidadPVCod_DTO.CodigoPlanVendido);
            if (planVendido == null)
            {
                return NotFound($"El codigo del plan al cual pertenece esta cuota no fue encontrado. Codigo: {PUT_EntidadPVCod_DTO.CodigoPlanVendido} no encontrado. Favor Verificar");
            }

            var entidadMapeada = mapper.Map(PUT_EntidadPVCod_DTO, cuota);


            // Log para verificar los valores actualizados en verde 
            Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Cuota actualizado: {entidadMapeada.Codigo}");
			Console.ResetColor();

			try
			{
				await repositorio.Update(cuota.Id, cuota);
				var cuotaDTO = mapper.Map<GET_CuotaDTO>(cuota);
				return Ok(cuotaDTO);
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

        // DELETE: --------------------------------------------------------------------
        [HttpDelete("EliminarCodigo/{codigo}")]
        public async Task<ActionResult> Delete(string codigo)
        {
            var existe = await repositorio.ExisteByCodigo(codigo);

            if (!existe)
            {
                return NotFound($"La Cuota con código {codigo} no existe");
            }

            var EntidadABorrar = await repositorio.SelectByCod(codigo);

            if (EntidadABorrar == null)
            {
                return NotFound($"No se encontró una Cuota con código {codigo}. Favor verificar");
            }

            if (await repositorio.Delete(EntidadABorrar.Id))
            {
                return Ok($"La Cuota con código {codigo} fue eliminado");
            }
            else
            {
                return BadRequest("No se pudo llevar a cabo la acción");
            }
        }

        [HttpDelete("EliminarListaCuotasByPlanVendidoCodigo/{codigo}")]
        public async Task<ActionResult> DeletByCodigoPV(string codigo)
        {
            var existe = await repositorio.ExisteConPVIncluido(codigo);
            if (!existe)
            {
                return NotFound($"Cuotas no asociadas a un Plan Vendido con el Codigo: {codigo}. Favor verificar");
            }

            if (await repositorio.DeleteByPVCod(codigo))
            {
                return Ok($"La Cuota con código {codigo} fue eliminado");
            }
            else
            {
                return BadRequest("No se pudo llevar a cabo la acción");
            }
        }
    }
}