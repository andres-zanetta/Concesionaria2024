using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.FacundoRepositorios;
using Concesionaria2024.Shared.DTO.FacundoDTO.Concesionaria2024.Shared.DTO.FacundoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.TipoPlan;
using Concesionaria2024.Shared.DTO.FacundoDTO.Vehiculo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Controllers.FacundoControllers
{
    [ApiController]
    [Route("api/TipoPlan")]
    public class TipoPlanController : ControllerBase
    {
        private readonly ITipoPlanRepositorio repositorio;
        private readonly IMapper mapper;

        public TipoPlanController( ITipoPlanRepositorio repositorio , IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tipoPlanes = await repositorio.Select();
                var tipoPlanesDTO = mapper.Map<List<GET_TipoPlanDTO>>(tipoPlanes);
                return Ok(tipoPlanesDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el método GET: {ex.Message}");
                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var tipoPlan = await repositorio.SelectById(id);
            if (tipoPlan == null)
            {
                return NotFound($"Tipo de plan con ID {id} no encontrado.");
            }
            var tipoPlanDTO = mapper.Map<GET_TipoPlanDTO>(tipoPlan);
            return Ok(tipoPlanDTO);
        }

        [HttpGet("existe/{id:int}")]
        public async Task<IActionResult> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return Ok(existe);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(POST_TipoPlanDTO entidadDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var entidad = mapper.Map<TipoPlan>(entidadDTO);
                var id = await repositorio.Insert(entidad);
                return CreatedAtAction(nameof(Get), new { id }, entidad);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Error al insertar en la base de datos: {ex.Message}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

		[HttpPut("CodigoAModificar/{codigo}")]
		public async Task<ActionResult> Put(string codigo, [FromBody] PUT_TipoPlanDTO entidadDTO)
		{
			if (!await repositorio.ExisteByCodigo(codigo))
			{
				return BadRequest($"No se encontró un vehiculo con el código {codigo}, compruebe el valor ingresado.");
			}

			var tipoPlan = await repositorio.SelectByCod(codigo);

			if (tipoPlan == null)
			{
				return NotFound($"No se encontró un tipoPlan con el código {codigo}, compruebe el valor ingresado.");
			}

			mapper.Map(entidadDTO, tipoPlan);

			// Log para verificar los valores actualizados en verde 
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"TipoPlan actualizado: {tipoPlan}");
			Console.ResetColor();

			try
			{
				await repositorio.Update(tipoPlan.Id, tipoPlan);
				var tipoPlanDTO = mapper.Map<GET_VehiculoDTO>(tipoPlan);
				return Ok(tipoPlanDTO);
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El tipo de plan con ID {id} no existe.");
            }

            if (await repositorio.Delete(id))
            {
                return Ok($"El tipo de plan con ID {id} fue eliminado.");
            }
            else
            {
                return BadRequest("No se pudo eliminar el tipo de plan.");
            }
        }
    }
}
