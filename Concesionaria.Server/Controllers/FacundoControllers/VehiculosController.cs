using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria.Server.Repositorio.FacundoRepositorios;
using Concesionaria2024.Shared.DTO.FacundoDTO.Concesionaria2024.Shared.DTO.FacundoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Vehiculo;
using Concesionaria2024.Shared.DTO.GinoDTO.Persona;
using Microsoft.AspNetCore.Mvc;

[Route("api/Vehiculos")]
[ApiController]
public class VehiculosController : ControllerBase
{
	private readonly IVehiculoRepositorio repositorio;
	private readonly Mapper mapper;

	public VehiculosController(IVehiculoRepositorio repositorio, Mapper mapper) 
    {
		this.repositorio = repositorio;
		this.mapper = mapper;
	}

    [HttpGet]
    public async Task<ActionResult<List<GET_VehiculoDTO>>> GetAll()
    {
        var vehiculos = await repositorio.Select();
        var vehiculosDTO = mapper.Map<List<GET_VehiculoDTO>>(vehiculos);
        return Ok(vehiculosDTO);
    }

    [HttpGet("Codigo/{string}")]
    public async Task<ActionResult<GET_VehiculoDTO>> GetByCod(string codigo)
    {
        try
        {
            Vehiculo vehiculo = await repositorio.SelectByCod(codigo);

			if (vehiculo == null)
			{
				return NotFound($"No existen vehiculos registrados con el codigo {codigo}");
			}
			var vehiculoDTO = mapper.Map<GET_PersonaDTO>(vehiculo);
			return Ok(vehiculoDTO);
		}
        catch (Exception ex)
        {

			if (ex.InnerException != null)
			{
				return BadRequest($"Error: {ex.Message}. Inner Exception: {ex.InnerException.Message}");
			}
			return BadRequest(ex.Message);
		}
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<GET_VehiculoDTO>> GetById(int id)
    {
        var vehiculo = await repositorio.SelectById(id);
        if (vehiculo == null)
        {
            return NotFound($"El vehículo con id {id} no se encontró.");
        }
        return Ok(mapper.Map<GET_VehiculoDTO>(vehiculo));
    }

    [HttpGet("existe/{id:int}")]
    public async Task<ActionResult<bool>> Exists(int id)
    {
        var existe = await repositorio.Existe(id);
        return Ok(existe);
    }

    [HttpPost]
    public async Task<ActionResult<string>> Post([FromBody] POST_VehiculoDTO entidadDTO)
    {
        if (entidadDTO == null)
        {
            return BadRequest("Los datos del vehículo son nulos.");
        }

        try
        {
            var vehiculo = mapper.Map<Vehiculo>(entidadDTO);

            var id = await repositorio.Insert(vehiculo);

            return Ok($"Vehiculo cargado correctamente. Codigo: {vehiculo.Codigo}");
        }
        catch (Exception e)
        {
            return BadRequest($"Error: {e.Message}");
        }
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] PUT_VehiculoDTO entidadDTO)
    {
        if (entidadDTO == null || id != entidadDTO.Id)
        {
            return BadRequest("Datos incorrectos.");
        }

        var vehiculo = await repositorio.SelectById(id);
        if (vehiculo == null)
        {
            return NotFound($"El vehículo con id {id} no existe.");
        }

        mapper.Map(entidadDTO, vehiculo);

        try
        {
            await repositorio.Update(id, vehiculo);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest($"Error: {e.Message}");
        }
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (!await repositorio.Existe(id))
        {
            return NotFound($"El vehículo con id {id} no existe.");
        }

        await repositorio.Delete(id);
        return NoContent();
    }
}
