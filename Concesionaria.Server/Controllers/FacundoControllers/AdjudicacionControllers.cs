using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.FacundoRepositorios;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.AndresDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Adjudicacion;
using Concesionaria2024.Shared.DTO.FacundoDTO.Concesionaria2024.Shared.DTO.FacundoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Vehiculo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Controllers
{
    [ApiController]
    [Route("api/Adjudicaciones")]
    public class AdjudicacionesController : ControllerBase
    {
        private readonly IPlanVendidoRepositorio planVendidoRepositorio;
        private readonly IAdjudicacionRepositorio repositorio;
        private readonly IMapper mapper;

        public AdjudicacionesController(IPlanVendidoRepositorio planVendidoRepositorio, IAdjudicacionRepositorio repositorio, IMapper mapper)
        {
            this.planVendidoRepositorio = planVendidoRepositorio;
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        // GET:-------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<List<GET_AdjudicacionDTO>>> Get()
        {
            try
            {
                var ListaAdjudicacion = await repositorio.SelectEntidadConVehiculo();
                var ListaAdjudicacionDTO = mapper.Map<List<GET_AdjudicacionDTO>>(ListaAdjudicacion);
                return Ok(ListaAdjudicacionDTO);
            }
            catch (Exception ex)
            {
                // Registrar el error para el diagnóstico
                Console.WriteLine($"Error en el método GET: {ex.Message}");
                return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
            }
        }

        [HttpGet("Patente/{patente}")]
        public async Task<ActionResult<GET_AdjudicacionDTO>> Get(string patente)
        {
            try
            {
                Adjudicacion? adjudicacion = await repositorio.SelectByPatente(patente);
                if (adjudicacion == null)
                {
                    return NotFound($"No se encontro una Adjudicación con un vehiculo cuya Patente sea: {patente}. Favor verificar");
                }
                var clienteDTO = mapper.Map<GET_AdjudicacionDTO>(adjudicacion);
                return Ok(clienteDTO);
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
        public async Task<ActionResult<GET_AdjudicacionDTO>> GetByCodigo(string codigo)
        {
            try
            {
                Adjudicacion? adjudicacion = await repositorio.SelectByCodigoConVehiculo(codigo);
                if (adjudicacion == null)
                {
                    return NotFound($"No se encontro  una Adjudicación con el codigo: {codigo}. Favor verificar");
                }
                var adjudicacionDTO = mapper.Map<GET_AdjudicacionDTO>(adjudicacion);
                return Ok(adjudicacionDTO);
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
        public async Task<ActionResult<int>> Post(POST_AdjudicacionDTO POST_entidadDTO)
        {
            try
            {
                if (POST_entidadDTO == null)
                {
                    return BadRequest("Los datos ingresados son nulos. Favor verificar.");
                }


                var VehiculoSeleccionado = await planVendidoRepositorio.SelectByCod(POST_entidadDTO.PlanVendidoCodigo);
                if (VehiculoSeleccionado == null)
                {
                    return NotFound($"No se encontró un Vehiculo asociado al Código: {POST_entidadDTO.PlanVendidoCodigo}. Favor verificar.");
                }

                Adjudicacion entidad = mapper.Map<Adjudicacion>(POST_entidadDTO);
                await repositorio.Insert(entidad);
                return Ok($"Adjudicación cargada correctamente. Codigo: {entidad.Codigo}, Patente del vehiculo adjudicado: {entidad.PatenteVehiculo}");
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
		public async Task<ActionResult> Put(string codigo, [FromBody] PUT_AdjudicacionDTO entidadDTO)
		{
			if (!await repositorio.ExisteByCodigo(codigo))
			{
				return BadRequest($"No se encontró un vehiculo con el código {codigo}, compruebe el valor ingresado.");
			}

			var adjudicacion = await repositorio.SelectByCod(codigo);

			if (adjudicacion == null)
			{
				return NotFound($"No se encontró un vehiculo con el código {codigo}, compruebe el valor ingresado.");
			}

			mapper.Map(entidadDTO, adjudicacion);

			// Log para verificar los valores actualizados en verde 
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Adjudicación actualizada: {adjudicacion.Codigo}-{adjudicacion.PatenteVehiculo}");
			Console.ResetColor();

			try
			{
				await repositorio.Update(adjudicacion.Id, adjudicacion);
				var adjudicacionDTO = mapper.Map<GET_AdjudicacionDTO>(adjudicacion);
				return Ok(adjudicacionDTO);
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
                return NotFound($"La Adjudicación con código {codigo} no existe");
            }

            var EntidadABorrar = await repositorio.SelectByCod(codigo);

            if (EntidadABorrar == null)
            {
                return NotFound($"No se encontró una Adjudicación con código {codigo}. Favor verificar");
            }

            if (await repositorio.Delete(EntidadABorrar.Id))
            {
                return Ok($"La Adjudicación con código {codigo} fue eliminada");
            }
            else
            {
                return BadRequest("No se pudo llevar a cabo la acción");
            }
        }
    }
}
