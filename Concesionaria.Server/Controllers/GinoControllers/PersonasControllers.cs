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
        private readonly IPersonaRepositorio repositorio;
        private readonly IMapper mapper;

        public PersonasControllers(IPersonaRepositorio repositorio, IMapper mapper)
        {
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
					return NotFound();
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
        public async Task<ActionResult<int>> Post(POST_PersonaDTO POST_entidadDTO)
        {
			if (POST_entidadDTO == null)
			{
				return BadRequest("Los datos de la persona se encuentran nulos. Favor verificar los campos");
			}

			try
            {
                Persona entidad = mapper.Map<Persona>(POST_entidadDTO);
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

		[HttpPut("CodigoAModificar/{codigo}")]
		public async Task<ActionResult> Put(string codigo, [FromBody] PUT_PersonaDTO entidadDTO)
		{
			if (!await repositorio.ExisteByCodigo(codigo))
			{
				return BadRequest($"No se encontró un vehiculo con el código {codigo}, compruebe el valor ingresado.");
			}

			var persona = await repositorio.SelectByCod(codigo);

			if (persona == null)
			{
				return NotFound($"No se encontró una persona con el código {codigo}, compruebe el valor ingresado.");
			}

			mapper.Map(entidadDTO, persona);

			// Log para verificar los valores actualizados en verde 
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Persona actualizada: {persona}");
			Console.ResetColor();

			try
			{
				await repositorio.Update(persona.Id, persona);
				var personaDTO = mapper.Map<GET_PersonaDTO>(persona);
				return Ok(personaDTO);
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
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"La persona {id} no existe");
            }
            Persona EntidadABorrar = new Persona();
            EntidadABorrar.Id = id;

            if (await repositorio.Delete(id))
            {
                return Ok($"La persona {id} fue eliminada");
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
