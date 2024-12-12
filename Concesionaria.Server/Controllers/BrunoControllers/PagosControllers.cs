using AutoMapper;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.BrunoRepositorios;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.AndresDTO;
using Concesionaria2024.Shared.DTO.BrunoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Concesionaria2024.Shared.DTO.FacundoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Vehiculo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Controllers.BrunoControllers
{
    [ApiController]
    [Route("api/Pagos")]
    public class PagosControllers : ControllerBase
    {
        private readonly ICuotaRepositorio repoCuota;
        private readonly IPagoRepositorio repositorio;
        private readonly IMapper mapper;

        public PagosControllers(ICuotaRepositorio repoCuota , IPagoRepositorio repositorio, IMapper mapper)
        {
            this.repoCuota = repoCuota;
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        // GET: -----------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<List<GET_PagoDTO>>> Get()
        {
            try
            {
                var ListClienteSelect = await repositorio.SelectAllConCuota();
                var ListCliente = mapper.Map<List<GET_PagoDTO>>(ListClienteSelect);
                return Ok(ListCliente);
            }
            catch (Exception ex)
            {
                // Registrar el error para el diagnóstico
                Console.WriteLine($"Error en el método GET: {ex.Message}");
                return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
            }
        }

        [HttpGet("CodigoPago/{codigo}")]
        public async Task<ActionResult<GET_PagoDTO>> Get(string codigo)
        {
            try
            {
                Pago? pago = await repositorio.SelectCodigoConCuota(codigo);
                if (pago == null)
                {
                    return NotFound($"No se encontro un Pago con el Código: {codigo}. Favor verificar");
                }
                var pagoDTO = mapper.Map<GET_PagoDTO>(pago);
                return Ok(pagoDTO);
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

        [HttpGet("ReferenciaPago/{refPago}")]
        public async Task<ActionResult<GET_PagoDTO>> GetByCodigo(string refPago)
        {
            try
            {
                Pago? pago = await repositorio.SelectByReferenciaPago(refPago);
                if (pago == null)
                {
                    return NotFound($"No se encontro un Pago con el codigo de la Referencia del Pago: {refPago}. Favor verificar");
                }
                var pagoDTO = mapper.Map<GET_PagoDTO>(pago);
                return Ok(pagoDTO);
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

        //POST: ----------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<int>> Post(POST_PagoDTO POST_entidadCuotaCodDTO)
        {
            try
            {
                if (POST_entidadCuotaCodDTO == null)
                {
                    return BadRequest("Los datos ingresados son nulos. Favor verificar.");
                }


                var CuotaSeleccionada = await repoCuota.SelectEntidadByCodConPagos(POST_entidadCuotaCodDTO.CodigoCuota);
                if (CuotaSeleccionada == null)
                {
                    return NotFound($"No se encontró una Persona asociada al Número de Docuemnto: {POST_entidadCuotaCodDTO.CodigoCuota}. Favor verificar.");
                }

                Pago entidad = mapper.Map<Pago>(POST_entidadCuotaCodDTO);
                await repositorio.Insert(entidad);
                return Ok($"Persona cargada correctamente. Codigo: {entidad.Codigo}, Fecha de Pago: {entidad.FechaPago}");
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

        // PUT: -----------------------------------------------------------------------
        [HttpPut("CodigoAModificar/{codigo}")]
		public async Task<ActionResult> Put(string codigo, [FromBody] PUT_PagoDTO PUT_entidadCuotaCodDTO)
		{
			if (!await repositorio.ExisteByCodigo(codigo))
			{
				return BadRequest($"No se encontró un vehiculo con el código {codigo}, compruebe el valor ingresado.");
			}

			var pagoSeleccionado = await repositorio.SelectByCod(codigo);

			if (pagoSeleccionado == null)
			{
				return NotFound($"No se encontró un pago con el código {codigo}, compruebe el valor ingresado.");
			}

            var cuota = await repoCuota.SelectByCod(PUT_entidadCuotaCodDTO.CodigoCuota);
            if (cuota == null)
            {
                return NotFound($"No se encontró una Persona con el Número de Docuemtno: {PUT_entidadCuotaCodDTO.CodigoCuota}. Favor verificar.");
            }

            var PagoModificado = mapper.Map(PUT_entidadCuotaCodDTO, pagoSeleccionado);

            // Log para verificar los valores actualizados en verde 
            Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"TipoDocumento actualizado: {pagoSeleccionado.Codigo}");
			Console.ResetColor();

			try
			{
				await repositorio.Update(pagoSeleccionado.Id, pagoSeleccionado);
				var pagoDTO = mapper.Map<GET_PagoDTO>(pagoSeleccionado);
				return Ok(pagoDTO);
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

        // DELETE: --------------------------------------------------------------------
        [HttpDelete("EliminarCodigo/{codigo}")]
        public async Task<ActionResult> Delete(string codigo)
        {
            var existe = await repositorio.ExisteByCodigo(codigo);

            if (!existe)
            {
                return NotFound($"El Pago con código {codigo} no existe");
            }

            var EntidadABorrar = await repositorio.SelectByCod(codigo);

            if (EntidadABorrar == null)
            {
                return NotFound($"No se encontró un Pago con código {codigo}. Favor verificar");
            }

            if (await repositorio.Delete(EntidadABorrar.Id))
            {
                return Ok($"El Pago con código {codigo} fue eliminada");
            }
            else
            {
                return BadRequest("No se pudo llevar a cabo la acción");
            }
        }
    }
}

