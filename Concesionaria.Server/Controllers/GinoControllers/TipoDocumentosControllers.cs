using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.GinoDTO;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Controllers.GinoControllers
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
        public async Task<ActionResult<List<GET_TipoDocumentoDTO>>> Get()
        {
            try
            {
                var tipoDocumento = await repositorio.Select();
                var tipoDocumentoDTO = mapper.Map<List<GET_TipoDocumentoDTO>>(tipoDocumento);
                return Ok(tipoDocumentoDTO);
            }
            catch (Exception ex)
            {
                // Registrar el error para el diagnóstico
                Console.WriteLine($"Error en el método GET: {ex.Message}");
                return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GET_TipoDocumentoDTO>> Get(int id)
        {
            TipoDocumento? tipoDocumento = await repositorio.SelectById(id);
            if (tipoDocumento == null)
            {
                return NotFound();
            }
            var tipoDocumentoDTO = mapper.Map<GET_TipoDocumentoDTO>(tipoDocumento);
            return Ok(tipoDocumentoDTO);
        }

        [HttpGet("cod/{cod}")]
        public async Task<ActionResult<GET_TipoDocumentoDTO>> GetByCod(string cod)
        {
            // para ver en la consola si funciona o no
            Console.WriteLine($"Buscando TipoDocumento con Codigo: {cod}");

            TipoDocumento? tipoDocumento = await repositorio.SelectByCod(cod);
            if (tipoDocumento == null)
            {
                return NotFound();
            }
            var tipoDocumentoDTO = mapper.Map<GET_TipoDocumentoDTO>(tipoDocumento);

            return Ok(tipoDocumentoDTO);
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }

        // POST ==========================================================================================
        [HttpPost]
        public async Task<ActionResult<int>> Post(POST_TipoDocumentoDTO POST_entidadDTO)
        {
            try
            {
                TipoDocumento entidad = mapper.Map<TipoDocumento>(POST_entidadDTO);
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
        public async Task<ActionResult> Put(int id, [FromBody] PUT_TipoDocumentoDTO PUT_EntidadDTO)
        {
            if (!await repositorio.Existe(id))
            {
                return BadRequest("No existe el tipo de documento buscado.");
            }

            var tipoDocumento = await repositorio.SelectById(id);

            if (tipoDocumento == null)
            {
                return NotFound("No existe el tipo de documento");
            }

            mapper.Map(PUT_EntidadDTO, tipoDocumento);

            // Log para verificar los valores actualizados
            Console.WriteLine($"TipoDocumento actualizado: {tipoDocumento.Nombre}, {tipoDocumento.Codigo}");

            try
            {
                await repositorio.Update(id, tipoDocumento);
                var tipoDocumentoDTO = mapper.Map<GET_TipoDocumentoDTO>(tipoDocumento);
                return Ok(tipoDocumentoDTO);
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
