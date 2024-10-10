using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.GinoDTO;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Controllers
{
    [ApiController]
    [Route("api/TipoDocumento")]
    public class TipoDocumentosController : ControllerBase
    {
        private readonly ITipoDocumentoRepositorio repositorio;
        private readonly IMapper mapper;

        public TipoDocumentosController(ITipoDocumentoRepositorio repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        // GET ==========================================================================================

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

        [HttpGet("cod/{cod}")]
        public async Task<ActionResult<TipoDocumento>> GetByCod(string cod)
        {
            // para ver en la consola si funciona o no
            Console.WriteLine($"Buscando TipoDocumento con Codigo: {cod}");

            TipoDocumento? sel = await repositorio.SelectByCod(cod);
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

        // POST ==========================================================================================
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

        // PUT ==========================================================================================

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ActualizarTipoDocumentoDTO ActualizarEntidadDTO)
        {
            if (!await repositorio.Existe(id))
            {
                return BadRequest("No existe el tipo de documento buscado.");
            }

            var sel = await repositorio.SelectById(id);

            if (sel == null)
            {
                return NotFound("No existe el tipo de documento");
            }

            mapper.Map(ActualizarEntidadDTO, sel);

            // Log para verificar los valores actualizados
            Console.WriteLine($"TipoDocumento actualizado: {sel.Nombre}, {sel.Codigo}");

            try
            {
                await repositorio.Update(id, sel);
                return Ok();
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
