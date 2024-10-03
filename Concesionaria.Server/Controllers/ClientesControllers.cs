using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.DB.Data;
using Concesionaria.Server.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Concesionaria2024.Shared.DTO;

namespace Concesionaria.Server.Controllers
{
    [ApiController]
    [Route("api/Cliente")]
    public class ClientesControllers:ControllerBase
    {
        public class ClientesController : ControllerBase
        {
            private readonly IRepositorio<Cliente> repositorio;
            private readonly IMapper mapper;
         

            public ClientesController(IRepositorio<Cliente> repositorio, IMapper mapper)
            {
                this.repositorio = repositorio;
                this.mapper = mapper;
                
            }

            // GET:-------------------------------------------------------------------
            [HttpGet] //api/Clientes
            public async Task<ActionResult<List<Cliente>>> Get()
            {
                return await repositorio.Select();
            }

            // GET: ID 
            [HttpGet("{ID:int}")]//api/Clientes/2
            public async Task<ActionResult<Cliente>> Get(int id)
            {
               Cliente? pepe = await repositorio.SelectById(id);
                if (pepe == null)
                {
                    return NotFound();
                }
                return pepe;
            }

            [HttpGet("existe/{id:int}")]
            public async Task<ActionResult<bool>> Existe(int id)
            {
                var existe = await repositorio.Existe(id);
                return existe;
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
            [HttpPut("{id:int}")]
            public async Task<ActionResult> Put(int id, [FromBody] Cliente cliente)
            {
                if (id != cliente.Id)
                {
                    return BadRequest("Datos incorrectos");
                }
                var sel = await repositorio.SelectById(id);

                if (sel == null)//sel es el nombre de la variable de cliente seleccionado
                {
                    return NotFound("No existe el cliente buscado.");
                }

                mapper.Map(cliente, sel);

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

            // DELETE: ID ----------------------------------------------------------------------
            [HttpDelete("{id:int}")]
            public async Task<ActionResult> Delete(int id)
            {
                var existe = await repositorio.Existe(id);
                if (!existe)
                {
                    return NotFound($"El cliente {id} no existe");
                }

                Cliente EntidadABorrar = new Cliente();
                EntidadABorrar.Id = id;

                if (await repositorio.Delete(id))
                {
                    return Ok($"El cliente {id} fue eliminado");
                }
                else
                {
                    return BadRequest();
                }
            }
        }

    }
}
