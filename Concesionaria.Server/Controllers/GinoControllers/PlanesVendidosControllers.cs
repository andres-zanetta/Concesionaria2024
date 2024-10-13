using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.FacundoRepositorios;
using Concesionaria.Server.Repositorio.GinoRepositorios;
using Concesionaria2024.Shared.DTO.GinoDTO;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Controllers.GinoControllers
{
    [ApiController]
    [Route("api/PlanVendido")]
    public class PlanesVendidosControllers : ControllerBase
    {
        private readonly IPlanVendidoRepositorio repositorio;
        private readonly IMapper mapper;

        public PlanesVendidosControllers(IPlanVendidoRepositorio repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }
                                  
        [HttpGet]
        public async Task<ActionResult<List<GET_PlanVendidoDTO>>> Get()
        {
            try
            {
                var ListPVendido = await repositorio.SelectPlanYAsociados();
                var ListPVendidoDTO = mapper.Map<List<GET_PlanVendidoDTO>>(ListPVendido);
                return Ok(ListPVendidoDTO);
            }
            catch (Exception e)
            {
                // Registrar el error para el diagnóstico
                Console.WriteLine($"Error en el método GET: {e.Message}");
                return StatusCode(500, $"Ocurrió un error interno: {e.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlanVendido>> Get(int id)
        {
            try
            {
                PlanVendido? planVendido = await repositorio.SelectPlanYAsociadosById(id);
                if (planVendido == null)
                {
                    return NotFound($"No se encontró el plan {id}");
                }
                var planVendidoDTO = mapper.Map<GET_PlanVendidoDTO>(planVendido);
                return Ok(planVendidoDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error en el método GET: {e.Message}");
                return StatusCode(500, $"Ocurrió un error interno: {e.Message}");
            }
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(POST_PlanVendidoDTO POST_entidadDTO)
        {
            try
            {
                PlanVendido entidad = mapper.Map<PlanVendido>(POST_entidadDTO);
                return Ok(await repositorio.Insert(entidad));
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
        public async Task<ActionResult> Put(int id, [FromBody] PlanVendido entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await repositorio.SelectById(id);

            if (sel == null)
            {
                return NotFound("No existe el plan buscado.");
            }

            mapper.Map(entidad, sel);

            try
            {
                await repositorio.Update(id, sel);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El plan {id} no existe");
            }
            PlanVendido EntidadABorrar = new PlanVendido();
            EntidadABorrar.Id = id;

            if (await repositorio.Delete(id))
            {
                return Ok($"El plan {id} fue eliminado");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
