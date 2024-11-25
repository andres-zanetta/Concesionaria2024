using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.FacundoDTO.Adjudicacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Controllers
{
    [ApiController]
    [Route("api/Adjudicaciones")]
    public class AdjudicacionesController : ControllerBase
    {
        private readonly IRepositorio<Adjudicacion> repositorio;
        private readonly IMapper mapper;

        public AdjudicacionesController(IRepositorio<Adjudicacion> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GET_AdjudicacionDTO>>> Get()
        {
            var adjudicaciones = await repositorio.Select();
            if (adjudicaciones == null || !adjudicaciones.Any())
            {
                return NotFound("No se encontraron adjudicaciones.");
            }

            var adjudicacionesDTO = mapper.Map<List<GET_AdjudicacionDTO>>(adjudicaciones);

            return Ok(adjudicacionesDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GET_AdjudicacionDTO>> Get(int id)
        {
            var adjudicacion = await repositorio.SelectById(id);
            if (adjudicacion == null)
            {
                return NotFound("La adjudicación no fue encontrada.");
            }

            var adjudicacionDTO = mapper.Map<GET_AdjudicacionDTO>(adjudicacion);
            return Ok(adjudicacionDTO);
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return Ok(existe);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(POST_AdjudicacionDTO entidadDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var entidad = mapper.Map<Adjudicacion>(entidadDTO);
                var result = await repositorio.Insert(entidad);
                return CreatedAtAction(nameof(Get), new { id = result }, result);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Error al insertar en la base de datos: {ex.Message}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] PUT_AdjudicacionDTO entidadDTO)
        {
            if (id != entidadDTO.Id)
            {
                return BadRequest("El ID proporcionado no coincide con el ID de la adjudicación.");
            }

            var adjudicacion = await repositorio.SelectById(id);
            if (adjudicacion == null)
            {
                return NotFound("La adjudicación no fue encontrada.");
            }

            mapper.Map(entidadDTO, adjudicacion);

            try
            {
                await repositorio.Update(id, adjudicacion);
                return Ok("La adjudicación fue actualizada correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al actualizar la adjudicación: {e.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"La adjudicación con ID {id} no existe.");
            }

            var success = await repositorio.Delete(id);
            if (success)
            {
                return Ok($"La adjudicación con ID {id} fue eliminada.");
            }
            else
            {
                return BadRequest("No se pudo eliminar la adjudicación.");
            }
        }
    }
}
