using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.GinoDTO;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Controllers.GinoControllers
{
    [ApiController]
    [Route("api/PlanVendido")]
    public class PlanesVendidosControllers : ControllerBase
    {
        private readonly IRepositorio<PlanVendido> repositorio;
        private readonly IMapper mapper;

        public PlanesVendidosControllers(IRepositorio<PlanVendido> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PlanVendido>>> Get()
        {
            return await repositorio.Select();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlanVendido>> Get(int id)
        {
            PlanVendido? sel = await repositorio.SelectById(id);
            if (sel == null)
            {
                return NotFound();
            }
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(POST_PlanVendidoDTO entidadDTO)
        {
            try
            {
                PlanVendido entidad = mapper.Map<PlanVendido>(entidadDTO);
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
