using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.FacundoDTO.Concesionaria2024.Shared.DTO.FacundoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Vehiculo;
using Concesionaria2024.Shared.DTO.GinoDTO.Persona;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Controllers.GinoControllers
{
	[ApiController]
	[Route("api/Persona")]
	public class PersonasControllers : ControllerBase
	{
		private readonly ITipoDocumentoRepositorio repoTipoDocumento;
		private readonly IPersonaRepositorio repositorio;
		private readonly IMapper mapper;

		public PersonasControllers( ITipoDocumentoRepositorio repoTipoDocumento ,IPersonaRepositorio repositorio, IMapper mapper)
		{
			this.repoTipoDocumento = repoTipoDocumento;
			this.repositorio = repositorio;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<List<GET_PersonaDTO>>> Get()
		{
			try
			{
				var ListPersonaSelect = await repositorio.SelectEntidadTD();
				var ListPersona = mapper.Map<List<GET_PersonaDTO>>(ListPersonaSelect);
				return Ok(ListPersona);
			}
			catch (Exception ex)
			{
				// Registrar el error para el diagnóstico
				Console.WriteLine($"Error en el método GET: {ex.Message}");
				return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
			}
		}

		[HttpGet("Documento/{documento}")]
		public async Task<ActionResult<GET_PersonaDTO>> Get(string documento)
		{
			try
			{
				Persona? persona = await repositorio.SelectByNumDoc(documento);
				if (persona == null)
				{
					return NotFound($"No se encontro una Persona con el número de docuemnto: {documento}. Favor verificar");
				}
				var personaDTO = mapper.Map<GET_PersonaDTO>(persona);
				return Ok(personaDTO);
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

		[HttpGet("Codigo/{codigo}")]
		public async Task<ActionResult<GET_PersonaDTO>> GetByCodigo(string codigo)
		{
			try
			{
				Persona? persona = await repositorio.SelectCodWhithTipoDoc(codigo);
				if (persona == null)
				{
					return NotFound($"No se encontro una Persona con el codigo: {codigo}. Favor verificar");
				}
				var personaDTO = mapper.Map<GET_PersonaDTO>(persona);
				return Ok(personaDTO);
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

		[HttpGet("existe/{documento}")]
		public async Task<ActionResult<bool>> Existe(string documento)
		{
			var existe = await repositorio.ExisteByDocumento(documento);
			if (!existe)
			{
				return NotFound($"La persona con el número de docuemnto {documento} no se encuentra registrada.");
			}
			return Ok(existe);
		}

		[HttpPost]
		public async Task<ActionResult<int>> Post(POST_PersonaNumDocDTO POST_entidadNumDocDTO)
		{
			try
			{
				if (POST_entidadNumDocDTO == null)
				{
					return BadRequest("Los datos ingresados son nulos. Favor verificar.");
				}


				// el bloque de 92 a 96 me permite validar si realmente existe el codigo seleccionado, cotejando con lo que está cargado en tipoDocumento.
				var tipoDoc = await repoTipoDocumento.SelectByCod(POST_entidadNumDocDTO.DocumentoCodigo);
				if (tipoDoc == null)
				{
					return NotFound($"No se encontró un Tipo de Documento con el codigo: {POST_entidadNumDocDTO.DocumentoCodigo}. Favor verificar.");
				}

				var POST_entidadDTO = mapper.Map<POST_PersonaDTO>(POST_entidadNumDocDTO);
				Persona personaDTO = mapper.Map<Persona>(POST_entidadDTO);

				await repositorio.Insert(personaDTO);
				return Ok($"Persona cargada correctamente. Codigo: {personaDTO.Codigo}, Número de docuemnto: {personaDTO.NumDoc}");

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

		[HttpPut("CodigoAModificar/{codigo}")]
		public async Task<ActionResult> Put(string codigo, [FromBody] PUT_PersonaNumDocDTO PUT_entidadNumDocDTO)
		{
			if (PUT_entidadNumDocDTO == null)
			{
				return BadRequest("Los datos ingresados son nulos. Favor verificar.");
			}

			var personaSeleccionada = await repositorio.SelectByCod(codigo);
			if (personaSeleccionada == null)
			{
				return NotFound($"No se encontró una Persona con el codigo: {codigo}. Favor verificar.");
			}

			var tipoDoc = await repoTipoDocumento.SelectByCod(PUT_entidadNumDocDTO.DocumentoCodigo);
			if (tipoDoc == null)
			{
				return NotFound($"No se encontró un Tipo de Documento con el código: {PUT_entidadNumDocDTO.DocumentoCodigo}. Favor verificar.");
			}

			//var PUT_entidadDTO = mapper.Map<PUT_PersonaDTO>(PUT_entidadNumDocDTO); <-- en esta línea perdia el Id de la Persona seleccionada a causa del mapeo
			// solución: modificar el mapeo

			var PersonaModificada = mapper.Map(PUT_entidadNumDocDTO, personaSeleccionada);

			// Log para verificar los valores actualizados en verde 

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Persona actualizada: {PersonaModificada.Id} - {PersonaModificada.Codigo} - {PersonaModificada.NumDoc}");
			Console.ResetColor();

			try
			{
				await repositorio.Update(PersonaModificada.Id, PersonaModificada);
				var GET_personaDTO = mapper.Map<GET_PersonaDTO>(PersonaModificada);
				return Ok(GET_personaDTO);

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

		[HttpDelete("EliminarCodigo/{codigo}")]
		public async Task<ActionResult> Delete(string codigo)
		{
			var existe = await repositorio.ExisteByCodigo(codigo);

			if (!existe)
			{
				return NotFound($"La Persona con código {codigo} no existe");
			}

			var EntidadABorrar = await repositorio.SelectByCod(codigo);

			if (EntidadABorrar == null)
			{
				return NotFound($"No se encontró una Persona con código {codigo}. Favor verificar");
			}

			if (await repositorio.Delete(EntidadABorrar.Id))
			{
				return Ok($"La Persona con código {codigo} fue eliminada");
			}
			else
			{
				return BadRequest("No se pudo llevar a cabo la acción");
			}
		}
	}
}
