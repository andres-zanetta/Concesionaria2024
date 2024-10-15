using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.FacundoDTO;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Controllers.FacundoControllers
{
    [ApiController]
    [Route("api/adjudicacion")]
    public class AdjudicacionController : ControllerBase
    {
        private readonly IAdjudicacionRepositorio repositorio;
        private readonly IMapper mapper;

        public AdjudicacionController(IAdjudicacionRepositorio repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CrearAdjudicacionDTO>>> Get()
        {
            try
            {
                var adjudicaciones = await repositorio.Select();
                var adjudicacionDtos = mapper.Map<List<CrearAdjudicacionDTO>>(adjudicaciones);
                return Ok(adjudicacionDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el método GET: {ex.Message}");
                return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CrearAdjudicacionDTO>> Get(int id)
        {
            var adjudicacion = await repositorio.SelectById(id);
            if (adjudicacion == null)
            {
                return NotFound("Adjudicación no encontrada.");
            }
            var adjudicacionDto = mapper.Map<CrearAdjudicacionDTO>(adjudicacion);
            return Ok(adjudicacionDto);
        }

        [HttpGet("vehiculo/{vehiculoId:int}")]
        public async Task<ActionResult<List<CrearAdjudicacionDTO>>> GetByVehiculoId(int vehiculoId)
        {
            var adjudicaciones = await repositorio.GetByVehiculoIdAsync(vehiculoId);
            var adjudicacionDtos = mapper.Map<List<CrearAdjudicacionDTO>>(adjudicaciones);
            return Ok(adjudicacionDtos);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CrearAdjudicacionDTO adjudicacionDto)
        {
            try
            {
                var adjudicacion = mapper.Map<Adjudicacion>(adjudicacionDto);
                var nuevoId = await repositorio.Insert(adjudicacion);
                return CreatedAtAction(nameof(Get), new { id = nuevoId }, adjudicacionDto);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}" + (e.InnerException != null ? $". Inner Exception: {e.InnerException.Message}" : ""));
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CrearAdjudicacionDTO adjudicacionDto)
        {
            if (!await repositorio.Existe(id))
            {
                return NotFound("No existe la adjudicación buscada.");
            }

            var adjudicacion = mapper.Map<Adjudicacion>(adjudicacionDto);
            if (id != adjudicacion.Id)
            {
                return BadRequest("El ID de la adjudicación no coincide.");
            }

            try
            {
                await repositorio.Update(id, adjudicacion);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}" + (e.InnerException != null ? $". Inner Exception: {e.InnerException.Message}" : ""));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await repositorio.Existe(id))
            {
                return NotFound($"La adjudicación {id} no existe.");
            }

            if (await repositorio.Delete(id))
            {
                return Ok($"La adjudicación {id} fue eliminada.");
            }
            else
            {
                return BadRequest("No se pudo eliminar la adjudicación.");
            }
        }
    }
}
