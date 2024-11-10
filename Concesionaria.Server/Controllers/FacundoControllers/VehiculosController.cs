using AutoMapper;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO.FacundoDTO.Concesionaria2024.Shared.DTO.FacundoDTO;
using Concesionaria2024.Shared.DTO.FacundoDTO.Vehiculo;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<GET_VehiculoDTO>>> GetAll()
    {
        var vehiculos = await repositorio.Select();
        return Ok(mapper.Map<List<GET_VehiculoDTO>>(vehiculos));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GET_VehiculoDTO>> GetById(int id)
    {
        var vehiculo = await repositorio.SelectById(id);
        if (vehiculo == null)
        {
            return NotFound();
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
    public async Task<ActionResult<int>> Post([FromBody] POST_VehiculoDTO entidadDTO)
    {
        if (entidadDTO == null)
        {
            return BadRequest("Los datos del vehículo son nulos.");
        }

        try
        {
            var vehiculo = mapper.Map<Vehiculo>(entidadDTO);
            var id = await repositorio.Insert(vehiculo);
            return CreatedAtAction(nameof(GetById), new { id }, vehiculo);
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
            return NotFound("No existe el vehículo buscado.");
        }

        mapper.Map(entidadDTO, vehiculo);

        try
        {
            await repositorio.Update(id, vehiculo);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (!await repositorio.Existe(id))
        {
            return NotFound($"El vehículo {id} no existe.");
        }

        await repositorio.Delete(id);
        return NoContent();
    }
}
