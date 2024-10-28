using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.FacundoDTO;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<GET_TipoPlanDTO>>> Get()
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
                return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GET_TipoPlanDTO>> Get(int id)
        {
            var tipoPlan = await repositorio.SelectById(id);
            if (tipoPlan == null)
            {
                return NotFound();
            }
            var tipoPlanDTO = mapper.Map<GET_TipoPlanDTO>(tipoPlan);
            return Ok(tipoPlanDTO);
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }

        // POST ==========================================================================================

        [HttpPost]
        public async Task<ActionResult<int>> Post(POST_TipoPlanDTO entidadDTO)
        {
            try
            {
                TipoPlan entidad = mapper.Map<TipoPlan>(entidadDTO);
                return await repositorio.Insert(entidad);
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

        // PUT ==========================================================================================

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] PUT_TipoPlanDTO entidadDTO)
        {
            if (!await repositorio.Existe(id))
            {
                return BadRequest("No existe el tipo de plan buscado.");
            }

            var tipoPlan = await repositorio.SelectById(id);
            if (tipoPlan == null)
            {
                return NotFound("No existe el tipo de plan.");
            }

            mapper.Map(entidadDTO, tipoPlan);

            try
            {
                await repositorio.Update(id, tipoPlan);
                var tipoPlanDTO = mapper.Map<GET_TipoPlanDTO>(tipoPlan);
                return Ok(tipoPlanDTO);
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

        // DELETE ==========================================================================================

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El tipo de plan {id} no existe");
            }

            if (await repositorio.Delete(id))
            {
                return Ok($"El tipo de plan {id} fue eliminado");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

