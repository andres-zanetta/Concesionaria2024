using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Controllers
{
    [ApiController]
    [Route("api/TipoDocumento")]
    public class TipoDocumentosController : ControllerBase
    {
        private readonly IRepositorio<TipoDocumento> repositorio;
        private readonly IMapper mapper;

        public TipoDocumentosController(IRepositorio<TipoDocumento> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoDocumento>>> Get()
        {
            return await repositorio.Select();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoDocumento>> Get(int id)
        {
            TipoDocumento? sel = await repositorio.SelectById(id);
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
        public async Task<ActionResult<int>> Post(CrearTipoDocumentoDTO entidadDTO)
        {
            try
            {
                TipoDocumento entidad = mapper.Map<TipoDocumento>(entidadDTO);
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoDocumento entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await repositorio.SelectById(id);

            if (sel == null)
            {
                return NotFound("No existe el tipo de documento buscado.");
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El documento {id} no existe");
            }
            TipoDocumento EntidadABorrar = new TipoDocumento();
            EntidadABorrar.Id = id;

            if (await repositorio.Delete(id))
            {
                return Ok($"El tipo de documento {id} fue eliminado");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
