using AutoMapper;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.BrunoRepositorios;
using Concesionaria2024.Shared.DTO.BrunoDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Controllers.BrunoControllers
{
    [ApiController]
    [Route("api/Pagos")]
    public class PagosControllers : ControllerBase
    {
        private readonly IPagoRepositorio repositorio;
        private readonly IMapper mapper;

        public PagosControllers(IPagoRepositorio repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        // GET: -----------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<List<Pago>>> Get()
        {
            return await repositorio.Select();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pago>> Get(int id)
        {
            Pago? sel = await repositorio.SelectById(id);
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
        public async Task<ActionResult<int>> Post(CrearPagoDTO entidadDTO)
        {
            try
            {
                Pago entidad = mapper.Map<Pago>(entidadDTO);
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
        public async Task<ActionResult> Put(int id, [FromBody] Pago entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await repositorio.SelectById(id);

            if (sel == null)
            {
                return NotFound("No existe el pago buscado.");
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
                return NotFound($"El pago {ID} no existe");
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

