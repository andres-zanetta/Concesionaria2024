using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.FacundoDTO.TipoPlan;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Controllers.FacundoControllers
{
    [ApiController]
    [Route("api/TipoPlan")]
    public class TipoPlanController : ControllerBase
    {
        private readonly IRepositorio<TipoPlan> repositorio;
        private readonly IMapper mapper;

        public TipoPlanController(IRepositorio<TipoPlan> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        // GET ==========================================================================================

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tipoPlanes = await repositorio.Select();
                var tipoPlanesDTO = mapper.Map<List<GET_TipoPlanDTO>>(tipoPlanes);
                return Ok(tipoPlanesDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el método GET: {ex.Message}");
                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var tipoPlan = await repositorio.SelectById(id);
            if (tipoPlan == null)
            {
                return NotFound($"Tipo de plan con ID {id} no encontrado.");
            }
            var tipoPlanDTO = mapper.Map<GET_TipoPlanDTO>(tipoPlan);
            return Ok(tipoPlanDTO);
        }

        [HttpGet("existe/{id:int}")]
        public async Task<IActionResult> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return Ok(existe);
        }

        // POST ==========================================================================================

        [HttpPost]
        public async Task<ActionResult<int>> Post(POST_TipoPlanDTO entidadDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var entidad = mapper.Map<TipoPlan>(entidadDTO);
                var id = await repositorio.Insert(entidad);
                return CreatedAtAction(nameof(Get), new { id }, entidad);
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


        // PUT ==========================================================================================

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] PUT_TipoPlanDTO entidadDTO)
        {
            if (entidadDTO == null)
            {
                return BadRequest("Los datos del tipo de plan son nulos.");
            }

            if (!await repositorio.Existe(id))
            {
                return NotFound($"Tipo de plan con ID {id} no encontrado.");
            }

            try
            {
                var tipoPlan = await repositorio.SelectById(id);
                if (tipoPlan == null)
                {
                    return NotFound($"Tipo de plan con ID {id} no encontrado.");
                }

                mapper.Map(entidadDTO, tipoPlan);
                await repositorio.Update(id, tipoPlan);
                var tipoPlanDTO = mapper.Map<GET_TipoPlanDTO>(tipoPlan);
                return Ok(tipoPlanDTO);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al actualizar: {e.Message}");
            }
        }

        // DELETE ==========================================================================================

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El tipo de plan con ID {id} no existe.");
            }

            if (await repositorio.Delete(id))
            {
                return Ok($"El tipo de plan con ID {id} fue eliminado.");
            }
            else
            {
                return BadRequest("No se pudo eliminar el tipo de plan.");
            }
        }
    }
}
