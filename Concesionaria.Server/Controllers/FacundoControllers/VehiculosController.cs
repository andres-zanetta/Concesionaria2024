using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.FacundoDTO;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Controllers.FacundoControllers
{
    [ApiController]
    [Route("api/Vehiculos")]
    public class VehiculosController : ControllerBase
    {
        private readonly IRepositorio<Vehiculo> repositorio;
        private readonly IMapper mapper;

        public VehiculosController(IRepositorio<Vehiculo> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Vehiculo>>> Get()
        {
            return await repositorio.Select();
        }
        // Obtener un vehiculo por ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Vehiculo>> Get(int id)
        {
            Vehiculo? sel = await repositorio.SelectById(id);
            if (sel == null)
            {
                return NotFound();
            }
            return sel;
        }
        // Verificar si un vehiculo existe
        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }
        // Crear un nuevo vehiculo
        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearVehiculoDTO entidadDTO)
        {
            try
            {
                Vehiculo entidad = mapper.Map<Vehiculo>(entidadDTO);
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
        // Actualizar un vehiculo existente
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Vehiculo entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await repositorio.SelectById(id);

            if (sel == null)
            {
                return NotFound("No existe el vehículo buscado.");
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
        // Eliminar un vehiculo por ID
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El vehículo {id} no existe");
            }
            Vehiculo EntidadABorrar = new Vehiculo { Id = id };

            if (await repositorio.Delete(id))
            {
                return Ok($"El vehículo {id} fue eliminado");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

