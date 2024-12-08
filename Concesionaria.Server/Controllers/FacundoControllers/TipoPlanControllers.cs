using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.FacundoRepositorios;
using Concesionaria2024.Shared.DTO.FacundoDTO.Concesionaria2024.Shared.DTO.FacundoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.TipoPlan;
using Concesionaria2024.Shared.DTO.FacundoDTO.Vehiculo;
using Concesionaria2024.Shared.DTO.GinoDTO.Persona;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Controllers.FacundoControllers
{
    [ApiController]
    [Route("api/TipoPlan")]
    public class TipoPlanController : ControllerBase
    {
		private readonly IVehiculoRepositorio vehiculoRepositorio;
		private readonly ITipoPlanRepositorio repositorio;
        private readonly IMapper mapper;

        public TipoPlanController( IVehiculoRepositorio vehiculoRepositorio, ITipoPlanRepositorio repositorio , IMapper mapper)
        {
			this.vehiculoRepositorio = vehiculoRepositorio;
			this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GET_TipoPlanDTO>>> GET()
        {
            try
            {
                var tipoPlanes = await repositorio.SelectWithVehiculo();
                var tipoPlanesDTO = mapper.Map<List<GET_TipoPlanDTO>>(tipoPlanes);
                return Ok(tipoPlanesDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el método GET: {ex.Message}");
                return StatusCode(500, "Ocurrió un error interno.");
            }
        }


		[HttpGet("Codigo/{codigo}")]
		public async Task<ActionResult<GET_TipoPlanDTO>> GetByCodigo(string codigo)
		{
			try
			{
				TipoPlan? tipoPlan = await repositorio.SelectCodWhithVehiculo(codigo);  // Revisar, el GetByCodigo no trae el codigo de vehiculo.
				if (tipoPlan == null)
				{
					return NotFound($"No se encontro una Persona con el codigo: {codigo}. Favor verificar");
				}
				var tipoPlanDTO = mapper.Map<GET_TipoPlanDTO>(tipoPlan);
				return Ok(tipoPlanDTO);
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

		[HttpPost]
        public async Task<ActionResult<int>> Post(POST_TipoPlanDTO POST_entidadCodVehDTO)
        {
			try
			{
				if (POST_entidadCodVehDTO == null)
				{
					return BadRequest("Los datos ingresados son nulos. Favor verificar.");
				}


				// el bloque de 92 a 96 me permite validar si realmente existe el codigo seleccionado, cotejando con lo que está cargado en tipoDocumento.
				var vehiculo = await vehiculoRepositorio.SelectByCod(POST_entidadCodVehDTO.CodigoVehiculo);
				if (vehiculo == null)
				{
					return NotFound($"No se encontró un Vehiculo con el codigo: {POST_entidadCodVehDTO.CodigoVehiculo}. Favor verificar.");
				}

				TipoPlan NuevoTipoPlan = mapper.Map<TipoPlan>(POST_entidadCodVehDTO);


				await repositorio.Insert(NuevoTipoPlan);
				return Ok($"Tipo de Plan cargado correctamente. Nombre del Plan: {NuevoTipoPlan.NombrePlan}, Codigo: {NuevoTipoPlan.Codigo}");

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
		public async Task<ActionResult> Put(string codigo, [FromBody] PUT_TipoPlanDTO PUT_entidadCodVehDTO)
		{
			if (PUT_entidadCodVehDTO == null)
			{
				return BadRequest("Los datos ingresados son nulos. Favor verificar.");
			}

			var tipoPlanSeleccionado = await repositorio.SelectCodWhithVehiculo(codigo);
			if (tipoPlanSeleccionado == null)
			{
				return NotFound($"No se encontró una Persona con el codigo: {codigo}. Favor verificar.");
			}

			var vehiculo = await vehiculoRepositorio.SelectByCod(PUT_entidadCodVehDTO.CodigoVehiculo);
			if (vehiculo == null)
			{
				return NotFound($"No se encontró un Tipo de Documento con el código: {PUT_entidadCodVehDTO.CodigoVehiculo}. Favor verificar.");
			}

			//var PUT_entidadDTO = mapper.Map<PUT_PersonaDTO>(PUT_entidadNumDocDTO); <-- en esta línea perdia el Id de la Persona seleccionada a causa del mapeo
			// solución: modificar el mapeo

			var tipoPlanModificado = mapper.Map(PUT_entidadCodVehDTO, tipoPlanSeleccionado);

			// Log para verificar los valores actualizados en verde 

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Persona actualizada: {tipoPlanModificado.Id} - {tipoPlanModificado.Codigo} - {tipoPlanModificado.NombrePlan}");
			Console.ResetColor();

			try
			{
				await repositorio.Update(tipoPlanModificado.Id, tipoPlanModificado);
				var GET_personaDTO = mapper.Map<GET_TipoPlanDTO>(tipoPlanModificado);
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

		[HttpDelete("Eliminar-Tipo-Plan/{codigo}")]
		public async Task<ActionResult> Delete(string codigo)
		{
			var existe = await repositorio.ExisteByCodigo(codigo);

			if (!existe)
			{
				return NotFound($"El Tipo de Plan con código {codigo} no existe");
			}

			var EntidadABorrar = await repositorio.SelectByCod(codigo);

			if (EntidadABorrar == null)
			{
				return NotFound($"No se encontró un Tipo de Plan con código {codigo}. Favor verificar");
			}

			if (await repositorio.Delete(EntidadABorrar.Id))
			{
				return Ok($"\"El Tipo de Plan con código {codigo} fue eliminado");
			}
			else
			{
				return BadRequest("No se pudo llevar a cabo la acción");
			}
		}
	}
}
