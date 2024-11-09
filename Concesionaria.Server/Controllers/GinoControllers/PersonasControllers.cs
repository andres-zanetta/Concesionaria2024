using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.GinoRepositorios;
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GET_PersonaDTO>> Get(int id)
        {
            Persona? persona = await repositorio.SelectEntidadTDById(id);
            if (persona == null)
            {
                return NotFound();
            }
            var personaDTO = mapper.Map<GET_PersonaDTO>(persona);
            return personaDTO;
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(POST_PersonaDTO POST_entidadDTO)
        {
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] PUT_PersonaDTO PUT_EntidadDTO)
        {
            if (!await repositorio.Existe(id))
            {
                return BadRequest("No existe el tipo de documento buscado.");
            }

            var persona = await repositorio.SelectEntidadTDById(id);

            if (persona == null)
            {
                return NotFound("No existe el tipo de documento");
            }

            mapper.Map(PUT_EntidadDTO, persona);

            // Log para verificar los valores actualizados en verde 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"TipoDocumento actualizado: {persona.Nombre}, {persona.Apellido}, {persona.NumDoc}, {persona.TipoDocumentoId}");
            Console.ResetColor();

            try
            {
                await repositorio.Update(id, persona);
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
