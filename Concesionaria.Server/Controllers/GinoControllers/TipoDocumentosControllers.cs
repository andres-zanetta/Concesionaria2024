using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.FacundoDTO.Concesionaria2024.Shared.DTO.FacundoDTO;
using Concesionaria2024.Shared.DTO.GinoDTO.Persona;
using Concesionaria2024.Shared.DTO.GinoDTO.TIpoDocumento;
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
                Console.WriteLine($"Error en el método GET: {ex.Message}");
                return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
            }
        }



        [HttpGet("Codigo/{codigo}")]
        public async Task<ActionResult<GET_TipoDocumentoDTO>> GetByCod(string codigo)
        {
			try
			{
				var tipoDocumento = await repositorio.SelectByCod(codigo);

				if (tipoDocumento == null)
				{
					return NotFound($"No existen vehiculos registrados con el codigo {codigo}");
				}
				var vehiculoDTO = mapper.Map<GET_TipoDocumentoDTO>(tipoDocumento);
				return Ok(vehiculoDTO);
			}
			catch (Exception ex)
			{

				if (ex.InnerException != null)
				{
					return BadRequest($"Error: {ex.Message}. Inner Exception: {ex.InnerException.Message}");
				}
				return BadRequest(ex.Message);
			}
		}

        [HttpGet("existe/{codigo}")]
        public async Task<ActionResult<bool>> Existe(string codigo)
        {
			var existe = await repositorio.ExisteByCodigo(codigo);
			if (!existe)
			{
				return NotFound($"El código {codigo} no existe.");
			}
            return Ok(existe);
		}

        // POST ==========================================================================================
        [HttpPost]
        public async Task<ActionResult<string>> Post(POST_TipoDocumentoDTO POST_entidadDTO)
        {
			if (POST_entidadDTO == null)
			{
				return BadRequest("Se está intentando ingresar valores nulos, favor verificar.");
			}

			try
			{
				var tipoDocumento = mapper.Map<TipoDocumento>(POST_entidadDTO);

				await repositorio.Insert(tipoDocumento);

				return Ok($"Tipo de documento cargado correctamente. Codigo: {tipoDocumento.Codigo}");
			}
			catch (Exception e)
			{
				return BadRequest($"Error: {e.Message}");
			}
		}

		// PUT ==========================================================================================

		[HttpPut("CodigoAModificar/{codigo}")]
		public async Task<ActionResult> Put(string codigo, [FromBody] PUT_TipoDocumentoDTO entidadDTO)
		{
			if (!await repositorio.ExisteByCodigo(codigo))
			{
				return BadRequest($"No se encontró un Tipo de Documento con el código {codigo}, compruebe el valor ingresado.");
			}

			var tipoDocumento = await repositorio.SelectByCod(codigo);

			if (tipoDocumento == null)
			{
				return NotFound($"No se encontró un Tipo de Documento con el código {codigo}, compruebe el valor ingresado.");
			}

			mapper.Map(entidadDTO, tipoDocumento);

			// Log para verificar los valores actualizados en verde 
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"tipoDocumento actualizada: {tipoDocumento.Codigo}, {tipoDocumento.Nombre}");
			Console.ResetColor();

			try
			{
				await repositorio.Update(tipoDocumento.Id, tipoDocumento);
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

		[HttpDelete("EliminarCodigo/{codigo}")]
		public async Task<ActionResult> Delete(string codigo)
		{
			var existe = await repositorio.ExisteByCodigo(codigo);

			if (!existe)
			{
				return NotFound($"El Tipo de Documento con código {codigo} no existe");
			}

			var EntidadABorrar = await repositorio.SelectByCod(codigo);

			if (EntidadABorrar == null)
			{
				return NotFound($"No se encontró un Tipo de Documento con código {codigo}. Favor verificar");
			}

			if (await repositorio.Delete(EntidadABorrar.Id))
			{
				return Ok($"El Tipo de Documento con código {codigo} fue eliminada");
			}
			else
			{
				return BadRequest("No se pudo llevar a cabo la acción");
			}
		}
	}
}
