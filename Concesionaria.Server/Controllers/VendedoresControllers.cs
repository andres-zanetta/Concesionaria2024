using AutoMapper;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concesionaria.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendedoresControllers : ControllerBase

    {
        private readonly IRepositorio<Vendedor> repositorio;
        private readonly IMapper mapper;

        public VendedoresControllers(IRepositorio<Vendedor> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }
        private readonly Context context;

        public VendedoresControllers(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Vendedor>>> Get()
        {
            return await context.Vendedores.ToListAsync();
        }

        // GET: ID 
        [HttpGet("{ID:int}")]
        public async Task<ActionResult<Vendedor>> Get(int ID)
        {
            var vendedor = await context.Vendedores.FirstOrDefaultAsync(x => x.Id == ID);

            if (vendedor == null)
            {
                return NotFound($"El vendedor con ID {ID} no fue encontrado.");
            }

            return vendedor;
        }

        // POST: ----------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearVendedorDTO entidadDTO)
        {
            try
            {
                Vendedor entidad = mapper.Map<Vendedor>(entidadDTO);
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
        public async Task<ActionResult> Put(int ID, [FromBody] Vendedor vendedor)
        {
            if (ID != vendedor.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await repositorio.SelectById(ID);

            if (sel == null)
            {
                return NotFound("No existe el vendedor buscado.");
            }

            mapper.Map(vendedor, sel);

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
                return NotFound($"El vendedor {ID} no existe");
            }

            if (await repositorio.Delete(ID))
            {
                return Ok($"El vendedor {ID} fue eliminado");
            }
            else
            {
                return BadRequest();
            }


        }
    }
}
