using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Controllers
{
    [ApiController]
    [Route("api/Adjudicacion")]
    public class AdjudicacionController : ControllerBase
    {
        private readonly IRepositorio<Adjudicacion> repositorio;
        private readonly IMapper mapper;

        public AdjudicacionController(IRepositorio<Adjudicacion> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }
        //Obtener Todas las Adjudicaciones
        [HttpGet]
        public async Task<ActionResult<List<Adjudicacion>>> Get()
        {
            return await repositorio.Select();
        }
        //Obtener una Adjudicacion por ID
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
        //Verificar si una Adjudicacion Existe
        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }
        //Crear una Nueva Adjudicacion
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
                if (e.InnerException != null)
                {
                    return BadRequest($"Error: {e.Message}. Inner Exception: {e.InnerException.Message}");
                }
                return BadRequest(e.Message);
            }
        }
        //Actualizar una Adjudicacion
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Adjudicacion entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await repositorio.SelectById(id);

            if (sel == null)
            {
                return NotFound("No existe la adjudicación buscada.");
            }

            mapper.Map(entidad, sel);

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
        //Eliminar
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"La adjudicación {id} no existe");
            }
            Adjudicacion entidadABorrar = new Adjudicacion { Id = id };

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

