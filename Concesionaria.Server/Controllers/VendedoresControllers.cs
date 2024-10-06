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
    [Route("api/Vendedores")]
    public class VendedoresControllers : ControllerBase

    {
        private readonly IRepositorio<Vendedor> repositorio;
        private readonly IMapper mapper;

        public VendedoresControllers(IRepositorio<Vendedor> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }
       
        [HttpGet]
        public async Task<ActionResult<List<Vendedor>>> Get()
        {
            return await repositorio.Select();
        }

        // GET: ID 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Vendedor>> Get(int id)
        {
            Vendedor? V = await repositorio.SelectById(id);
           
            if (V == null)
            {
                return NotFound($"El vendedor con ID {id} no fue encontrado.");
            }

            return V;
        }

        [HttpGet("existe/{id:int}")]

        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
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
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Vendedor entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await repositorio.SelectById(id);

            if (sel == null)
            {
                return NotFound("No existe el vendedor buscado.");
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

        // DELETE: ID ----------------------------------------------------------------------
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El vendedor {id} no existe");
            }
            Vendedor EntidadABorrar = new Vendedor();
            EntidadABorrar.Id = id;

            if (await repositorio.Delete(id))
            {
                return Ok($"El vendedor {id} fue eliminado");
            }
            else
            {
                return BadRequest();
            }


        }
    }
}
