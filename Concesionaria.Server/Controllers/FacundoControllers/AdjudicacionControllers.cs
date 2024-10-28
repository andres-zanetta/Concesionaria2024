using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.FacundoDTO;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<Adjudicacion>>> Get()
        {
            return await repositorio.Select();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Adjudicacion>> Get(int id)
        {
            Adjudicacion? sel = await repositorio.SelectById(id);
            if (sel == null)
            {
                return NotFound();
            }
            return sel;
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearAdjudicacionDTO entidadDTO)
        {
            try
            {
                Adjudicacion entidad = mapper.Map<Adjudicacion>(entidadDTO);
                return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException != null
                    ? $"Error: {e.Message}. Inner Exception: {e.InnerException.Message}"
                    : e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] PUT_AdjudicacionDTO entidadDTO)
        {
            if (id != entidadDTO.Id)
            {
                return BadRequest("Datos incorrectos");
            }

            var sel = await repositorio.SelectById(id);
            if (sel == null)
            {
                return NotFound("No existe la adjudicación buscada.");
            }

            mapper.Map(entidadDTO, sel);

            try
            {
                await repositorio.Update(id, sel);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"La adjudicación {id} no existe");
            }

            if (await repositorio.Delete(id))
            {
                return Ok($"La adjudicación {id} fue eliminada");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

