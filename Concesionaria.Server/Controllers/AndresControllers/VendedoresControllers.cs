using AutoMapper;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.AndresRepositorios;
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
        private readonly IVendedorRepositorio repositorio;
        private readonly IMapper mapper;

        public VendedoresControllers(IVendedorRepositorio repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GET_VendedorDTO>>> Get()
        {
            try
            {
                var ListPersonaSelect = await repositorio.Select();
                var ListPersona = mapper.Map<List<GET_VendedorDTO>>(ListPersonaSelect);
                return Ok(ListPersona);
            }
            catch (Exception ex)
            {
                // Registrar el error para el diagnóstico
                Console.WriteLine($"Error en el método GET: {ex.Message}");
                return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
            }
        }

        // GET: ID 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GET_VendedorDTO>> Get(int id)
        {
            Vendedor? vendedor = await repositorio.SelectById(id);

            if (vendedor == null)
            {
                return NotFound($"El vendedor con ID {id} no fue encontrado.");
            }
            var VendedorDTO = mapper.Map<GET_VendedorDTO>(vendedor);

            return VendedorDTO;
        }




        //[HttpGet("GetByFecha/{fecha:Datetime}")]
        //public async Task<ActionResult<Vendedor>> GetByFecha(DateTime fecha)
        //{
        //    Vendedor? V= await repositorio.SelectByFecha(fecha);
        //    if(V==null)
        //    {
        //        return NotFound();
        //    }
        //    return V;
            
        //}

        


        [HttpGet("existe/{id:int}")]

        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }
        // POST: ----------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<int>> Post(POST_VendedorSinFechainicio vendedorDTO)
        {
            var DTOVendedor = new POST_VendedorDTO
            {
                CantPlanesVendidos = vendedorDTO.CantPlanesVendidos,
                FechaInicio = DateTime.Today,
                PersonaId = vendedorDTO.PersonaId,
            };

            try
            {
                Vendedor entidad = mapper.Map<Vendedor>(DTOVendedor);
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

		// PUT: ID ------------------------------------------------------------------------
		[HttpPut("CodigoAModificar/{codigo}")]
		public async Task<ActionResult> Put(string codigo, [FromBody] PUT_VendedorDTO entidadDTO)
		{
			if (!await repositorio.ExisteByCodigo(codigo))
			{
				return BadRequest($"No se encontró un vehiculo con el código {codigo}, compruebe el valor ingresado.");
			}

			var vendedor = await repositorio.SelectByCod(codigo);

			if (vendedor == null)
			{
				return NotFound($"No se encontró un vendedor con el código {codigo}, compruebe el valor ingresado.");
			}

			mapper.Map(entidadDTO, vendedor);

			// Log para verificar los valores actualizados en verde 
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"TipoDocumento actualizado: {vendedor}");
			Console.ResetColor();

			try
			{
				await repositorio.Update(vendedor.Id, vendedor);
				var vendedorDTO = mapper.Map<PUT_VendedorDTO>(vendedor);
				return Ok(vendedorDTO);
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

        // DELETE: ID ----------------------------------------------------------------------
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El vendedor {id} no existe");
            }
            Vendedor EntidadABorrar = new Vendedor();
            EntidadABorrar.Id = id;

            if (await repositorio.Delete(id))
            {
                return Ok($"El vendedor {id} fue eliminado");
            }
            else
            {
                return BadRequest();
            }


        }
    }
}
