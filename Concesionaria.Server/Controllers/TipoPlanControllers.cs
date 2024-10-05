using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Controllers
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
        // Obtener todos los tipos de plan
        [HttpGet]
        public async Task<ActionResult<List<TipoPlan>>> Get()
        {
            return await repositorio.Select();
        }
        // Obtener un tipo de plan por su ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoPlan>> Get(int id)
        {
            TipoPlan? sel = await repositorio.SelectById(id);
            if (sel == null)
            {
                return NotFound();
            }
            return sel;
        }
        // Verificar si un tipo de plan existe por su ID
        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }
        // Crear un nuevo tipo de plan
        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearTipoPlanDTO entidadDTO)
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
        // Actualizar un tipo de plan existente
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoPlan entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await repositorio.SelectById(id);

            if (sel == null)
            {
                return NotFound("No existe el tipo de plan buscado.");
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
        // Eliminar un tipo de plan por ID
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El tipo de plan {id} no existe");
            }
            TipoPlan entidadABorrar = new TipoPlan { Id = id };

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

