using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.AndresRepositorios;
using Microsoft.AspNetCore.Mvc;
using Concesionaria2024.Shared.DTO.AndresDTO;
using Concesionaria2024.Shared.DTO.GinoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Concesionaria2024.Shared.DTO.FacundoDTO;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.GinoDTO.Persona;

namespace Concesionaria.Server.Controllers.AndresControllers
{
    [ApiController]
    [Route("api/Cliente")]
    public class ClientesController : ControllerBase
    {
		private readonly IPersonaRepositorio personaRepositorio;
		private readonly IClienteRepositorio repositorio;
        private readonly IMapper mapper;


        public ClientesController( IPersonaRepositorio personaRepositorio, IClienteRepositorio repositorio, IMapper mapper)
        {
			this.personaRepositorio = personaRepositorio;
			this.repositorio = repositorio;
            this.mapper = mapper;

        }

		// GET:-------------------------------------------------------------------
		[HttpGet]
		public async Task<ActionResult<List<GET_ClienteDTO>>> Get()
		{
			try
			{
				var ListClienteSelect = await repositorio.SelectConPersona();
				var ListCliente = mapper.Map<List<GET_ClienteDTO>>(ListClienteSelect);
				return Ok(ListCliente);
			}
			catch (Exception ex)
			{
				// Registrar el error para el diagnóstico
				Console.WriteLine($"Error en el método GET: {ex.Message}");
				return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
			}
		}

		[HttpGet("Documento/{documento}")]
		public async Task<ActionResult<GET_ClienteDTO>> Get(string documento)
		{
			try
			{
				Cliente? cliente = await repositorio.SelectByDNI(documento);
				if (cliente == null)
				{
					return NotFound($"No se encontro un Cliente con el número de docuemnto: {documento}. Favor verificar");
				}
				var clienteDTO = mapper.Map<GET_ClienteDTO>(cliente);
				return Ok(clienteDTO);
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
		public async Task<ActionResult<GET_ClienteDTO>> GetByCodigo(string codigo)
		{
			try
			{
				Cliente? cliente = await repositorio.SelectByCodConPersona(codigo);
				if (cliente == null)
				{
					return NotFound($"No se encontro un Cliente con el codigo: {codigo}. Favor verificar");
				}
				var clienteDTO = mapper.Map<GET_ClienteDTO>(cliente);
				return Ok(clienteDTO);
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

		// POST: ----------------------------------------------------------------------
		[HttpPost]
        public async Task<ActionResult<int>> Post(POST_ClienteConNumDocDTO POST_EntidadConNumDocDTO)
        {
			try
            {
				if (POST_EntidadConNumDocDTO == null)
				{
					return BadRequest("Los datos ingresados son nulos. Favor verificar.");
				}


				var personaConTipoDoc = await personaRepositorio.SelectByNumDoc(POST_EntidadConNumDocDTO.NumDoc);
				if (personaConTipoDoc == null)
				{
					return NotFound($"No se encontró una Persona asociada al Número de Docuemnto: {POST_EntidadConNumDocDTO.NumDoc}. Favor verificar.");
				}

				Cliente entidad = mapper.Map<Cliente>(POST_EntidadConNumDocDTO);
                await repositorio.Insert(entidad);
				return Ok($"Persona cargada correctamente. Codigo: {entidad.Codigo}, Fecha de Inicio: {entidad.FechaInicio}");
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

		// PUT ------------------------------------------------------------------------
		[HttpPut("CodigoAModificar/{codigo}")]
		public async Task<ActionResult> Put(string codigo, [FromBody] PUT_ClienteDTO PUT_entidadNumDocDTO)
        {
			if (PUT_entidadNumDocDTO == null)
			{
				return BadRequest("Los datos ingresados son nulos. Favor verificar.");
			}

			var clienteSeleccionado = await repositorio.SelectByCod(codigo);
			if (clienteSeleccionado == null)
			{
				return NotFound($"No se encontró un Cliente con el codigo: {codigo}. Favor verificar.");
			}

			var persona = await personaRepositorio.SelectByNumDoc(PUT_entidadNumDocDTO.NumDoc);
			if (persona == null)
			{
				return NotFound($"No se encontró una Persona con el Número de Docuemtno: {PUT_entidadNumDocDTO.NumDoc}. Favor verificar.");
			}

			//var PUT_entidadDTO = mapper.Map<PUT_PersonaDTO>(PUT_entidadNumDocDTO); <-- en esta línea perdia el Id de la Persona seleccionada a causa del mapeo
			// solución: modificar el mapeo

			var ClienteModificado = mapper.Map(PUT_entidadNumDocDTO, clienteSeleccionado);

			// Log para verificar los valores actualizados en verde 

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Persona actualizada: {ClienteModificado.Id} - {ClienteModificado.Codigo}");
			Console.ResetColor();

			try
			{
				await repositorio.Update(ClienteModificado.Id, ClienteModificado);
				var ClienteDTO = mapper.Map<GET_ClienteDTO>(ClienteModificado);
				return Ok(ClienteDTO);

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
				return NotFound($"El Cliente con código {codigo} no existe");
			}

			var EntidadABorrar = await repositorio.SelectByCod(codigo);

			if (EntidadABorrar == null)
			{
				return NotFound($"No se encontró un Cliente con código {codigo}. Favor verificar");
			}

			if (await repositorio.Delete(EntidadABorrar.Id))
			{
				return Ok($"El Cliente con código {codigo} fue eliminada");
			}
			else
			{
				return BadRequest("No se pudo llevar a cabo la acción");
			}
		}
	}
}