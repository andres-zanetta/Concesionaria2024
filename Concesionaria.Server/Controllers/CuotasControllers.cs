using AutoMapper;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Controllers
{
    [ApiController]
    [Route("api/Cuotas")]
    public class CuotasControllers : ControllerBase
    {
        private readonly IRepositorio<Cuota> repositorio;
        private readonly IMapper mapper;

        public CuotasControllers(IRepositorio<Cuota> repositorio, IMapper mapper)
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
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Cuota entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await repositorio.SelectById(id);

            if (sel == null)
            {
                return NotFound("No existe la cuota buscada.");
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