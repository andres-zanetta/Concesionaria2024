using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.DB.Data;
using Concesionaria.Server.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Concesionaria2024.Shared.DTO;

namespace Concesionaria.Server.Controllers
{
    public class ClientesControllers:ControllerBase

    {
        [ApiController]
        [Route("api/[controller]")]
        public class ClienteController : ControllerBase
        {
            private readonly IRepositorio<Cliente> repositorio;
            private readonly IMapper mapper;
            private readonly Context context;

            public ClienteController(IRepositorio<Cliente> repositorio, IMapper mapper, Context context)
            {
                this.repositorio = repositorio;
                this.mapper = mapper;
                this.context = context;
            }

            // GET:-------------------------------------------------------------------
            [HttpGet]
            public async Task<ActionResult<List<Cliente>>> Get()
            {
                return await context.Clientes.ToListAsync();
            }

            // GET: ID 
            [HttpGet("{ID:int}")]
            public async Task<ActionResult<Cliente>> Get(int ID)
            {
                var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == ID);

                if (cliente == null)
                {
                    return NotFound($"El cliente con ID {ID} no fue encontrado.");
                }

                return cliente;
            }

            // POST: ----------------------------------------------------------------------
            [HttpPost]
            public async Task<ActionResult<int>> Post(CrearClienteDTO entidadDTO)
            {
                try
                {
                    Cliente entidad = mapper.Map<Cliente>(entidadDTO);
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
            public async Task<ActionResult> Put(int ID, [FromBody] Cliente cliente)
            {
                if (ID != cliente.Id)
                {
                    return BadRequest("Datos incorrectos");
                }
                var sel = await repositorio.SelectById(ID);

                if (sel == null)
                {
                    return NotFound("No existe el cliente buscado.");
                }

                mapper.Map(cliente, sel);

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
                    return NotFound($"El cliente {ID} no existe");
                }

                if (await repositorio.Delete(ID))
                {
                    return Ok($"El cliente {ID} fue eliminado");
                }
                else
                {
                    return BadRequest();
                }
            }
        }

    }
}
