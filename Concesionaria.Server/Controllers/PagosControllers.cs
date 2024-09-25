using AutoMapper;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concesionaria.Server
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosControllers : ControllerBase
    {
        private readonly IRepositorio<Pago> repositorio;
        private readonly IMapper mapper;

        public PagosControllers(IRepositorio<Pago> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }
        private readonly Context context;


        public PagosControllers(Context context)
        {
            this.context = context;
        }

        // GET:-------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<List<Pago>>> Get()
        {
            return await context.Pagos
                .Include(p => p.Cuota)
                .ToListAsync();
        }

        // GET: ID 
        [HttpGet("{ID:int}")]
        public async Task<ActionResult<Pago>> Get(int ID)
        {
            var pago = await context.Pagos
                .Include(p => p.Cuota)
                .FirstOrDefaultAsync(x => x.Id == ID);

            if (pago == null)
            {
                return NotFound($"El pago con ID {ID} no fue encontrado.");
            }

            return pago;
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

        // PUT: ID ------------------------------------------------------------------------
        [HttpPut("{ID:int}")]
        public async Task<ActionResult> Put(int ID, [FromBody] Pago pago)
        {
            if (ID != pago.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await repositorio.SelectById(ID);

            if (sel == null)
            {
                return NotFound("No existe el tipo de documento buscado.");
            }

            mapper.Map(pago, sel);

            try
            {
                await repositorio.Update(ID, sel);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: ID ----------------------------------------------------------------------
        [HttpDelete("{ID:int}")]
        public async Task<ActionResult> Delete(int ID)
        {
            var existe = await repositorio.Existe(ID);
            if (!existe)
            {
                return NotFound($"El documento {ID} no existe");
            }
            TipoDocumento EntidadABorrar = new TipoDocumento();
            EntidadABorrar.Id = ID;

            if (await repositorio.Delete(ID))
            {
                return Ok($"El tipo de documento {ID} fue eliminado");
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
