using AutoMapper;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.AndresRepositorios;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.AndresDTO;
using Concesionaria2024.Shared.DTO.GinoDTO;
using Concesionaria2024.Shared.DTO.GinoDTO.Persona;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concesionaria.Server.Controllers.AndresControllers
{
    [ApiController]
    [Route("api/Vendedores")]
    public class VendedoresControllers : ControllerBase

    {
		private readonly IPersonaRepositorio personaRepositorio;
		private readonly IVendedorRepositorio repositorio;
        private readonly IMapper mapper;

        public VendedoresControllers(IPersonaRepositorio personaRepositorio, IVendedorRepositorio repositorio, IMapper mapper)
        {
			this.personaRepositorio = personaRepositorio;
			this.repositorio = repositorio;
            this.mapper = mapper;
        }

		// GET:-------------------------------------------------------------------
		[HttpGet]
		public async Task<ActionResult<List<GET_VendedorDTO>>> Get()
		{
			try
			{
				var ListClienteSelect = await repositorio.SelectConPersona();
				var ListCliente = mapper.Map<List<GET_VendedorDTO>>(ListClienteSelect);
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
		public async Task<ActionResult<GET_VendedorDTO>> Get(string documento)
		{
			try
			{
				Vendedor? vendedor = await repositorio.SelectByDNI(documento);
				if (vendedor == null)
				{
					return NotFound($"No se encontro un Vendedor con el número de docuemnto: {documento}. Favor verificar");
				}
				var vendedorDTO = mapper.Map<GET_VendedorDTO>(vendedor);
				return Ok(vendedorDTO);
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
		public async Task<ActionResult<GET_VendedorDTO>> GetByCodigo(string codigo)
		{
			try
			{
				Vendedor? vendedor = await repositorio.SelectByCodConPersona(codigo);
				if (vendedor == null)
				{
					return NotFound($"No se encontro un Vendedor con el codigo: {codigo}. Favor verificar");
				}
				var vendedorDTO = mapper.Map<GET_VendedorDTO>(vendedor);
				return Ok(vendedorDTO);
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
		public async Task<ActionResult<int>> Post(POST_VendedorDTO POST_EntidadConNumDocDTO)
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

				Vendedor entidad = mapper.Map<Vendedor>(POST_EntidadConNumDocDTO);
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
		public async Task<ActionResult> Put(string codigo, [FromBody] PUT_VendedorDTO PUT_entidadNumDocDTO)
		{
			if (PUT_entidadNumDocDTO == null)
			{
				return BadRequest("Los datos ingresados son nulos. Favor verificar.");
			}

			var vendedorSeleccionado = await repositorio.SelectByCod(codigo);
			if (vendedorSeleccionado == null)
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

			var vendedorModificado = mapper.Map(PUT_entidadNumDocDTO, vendedorSeleccionado);

			// Log para verificar los valores actualizados en verde 

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Persona actualizada: {vendedorModificado.Id} - {vendedorModificado.Codigo}");
			Console.ResetColor();

			try
			{
				await repositorio.Update(vendedorModificado.Id, vendedorModificado);
				var VendedorDTO = mapper.Map<GET_VendedorDTO>(vendedorModificado);
				return Ok(VendedorDTO);

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
				return NotFound($"El Vendedor con código {codigo} no existe");
			}

			var EntidadABorrar = await repositorio.SelectByCod(codigo);

			if (EntidadABorrar == null)
			{
				return NotFound($"No se encontró un Vendedor con código {codigo}. Favor verificar");
			}

			if (await repositorio.Delete(EntidadABorrar.Id))
			{
				return Ok($"El Vendedor con código {codigo} fue eliminada");
			}
			else
			{
				return BadRequest("No se pudo llevar a cabo la acción");
			}
		}
	}
}
