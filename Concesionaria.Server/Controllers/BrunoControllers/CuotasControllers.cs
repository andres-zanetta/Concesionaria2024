using AutoMapper;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.BrunoRepositorios;
using Concesionaria2024.Shared.DTO.BrunoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Concesionaria2024.Shared.DTO.FacundoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Vehiculo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Controllers.BrunoControllers
{
    [ApiController]
    [Route("api/Cuotas")]
    public class CuotasControllers : ControllerBase
    {
        private readonly ICuotaRepositorio repositorio;
        private readonly IMapper mapper;

        public CuotasControllers(ICuotaRepositorio repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }


        // GET: -----------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<List<Cuota>>> Get()
        {
            return await repositorio.Select();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cuota>> Get(int id)
        {
            Cuota? sel = await repositorio.SelectById(id);
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

        // POST: ----------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearCuotaDTO entidadDTO)
        {
            try
            {
                Cuota entidad = mapper.Map<Cuota>(entidadDTO);
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

		// PUT: -----------------------------------------------------------------------
		[HttpPut("CodigoAModificar/{codigo}")]
		public async Task<ActionResult> Put(string codigo, [FromBody] PUT_CuotaDTO entidadDTO)
		{
			if (!await repositorio.ExisteByCodigo(codigo))
			{
				return BadRequest($"No se encontró un vehiculo con el código {codigo}, compruebe el valor ingresado.");
			}

			var cuota = await repositorio.SelectByCod(codigo);

			if (cuota == null)
			{
				return NotFound($"No se encontró una cuota con el código {codigo}, compruebe el valor ingresado.");
			}

			mapper.Map(entidadDTO, cuota);

			// Log para verificar los valores actualizados en verde 
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"TipoDocumento actualizado: {cuota}");
			Console.ResetColor();

			try
			{
				await repositorio.Update(cuota.Id, cuota);
				var cuotaDTO = mapper.Map<PUT_CuotaDTO>(cuota);
				return Ok(cuotaDTO);
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
        [HttpDelete("{ID:int}")]
        public async Task<ActionResult> Delete(int ID)
        {
            var existe = await repositorio.Existe(ID);
            if (!existe)
            {
                return NotFound($"La cuota {ID} no existe");
            }

            if (await repositorio.Delete(ID))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}