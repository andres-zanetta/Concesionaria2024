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
	private readonly IMapper mapper;

	public VehiculosController(IVehiculoRepositorio repositorio, IMapper mapper) 
    {
		this.repositorio = repositorio;
		this.mapper = mapper;
	}

    [HttpGet]
    public async Task<ActionResult<List<GET_VehiculoDTO>>> Get()
    {
		try
		{
			var vehiculos = await repositorio.Select();
			var vehiculosDTO = mapper.Map<List<GET_VehiculoDTO>>(vehiculos);
			return Ok(vehiculosDTO);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error en el método GET: {ex.Message}");
			return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
		}
 
    }

    [HttpGet("Marca/{marca}")]
    public async Task<ActionResult<List<Vehiculo>>> GetByMarca(string marca)
    {
		try
		{
			var vehiculos = await repositorio.SelectByMarca(marca);
			var vehiculosDTO = mapper.Map<List<GET_VehiculoDTO>>(vehiculos);
			return Ok(vehiculosDTO);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error en el método GET: {ex.Message}");
			return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
		}

	}

    [HttpGet("Codigo/{codigo}")]
    public async Task<ActionResult<GET_VehiculoDTO>> GetByCod(string codigo)
    {
        try
        {
            Vehiculo vehiculo = await repositorio.SelectByCod(codigo);

			if (vehiculo == null)
			{
				return NotFound($"No existen vehiculos registrados con el codigo {codigo}");
			}
			var vehiculoDTO = mapper.Map<GET_VehiculoDTO>(vehiculo);
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

    [HttpGet("existe/{codigo}")]
    public async Task<ActionResult<bool>> ExisteByCod(string codigo)
    {
        var existe = await repositorio.ExisteByCodigo(codigo);
		if (!existe)
		{
			return NotFound($"El código {codigo} no existe.");
		}
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

            await repositorio.Insert(vehiculo);

            return Ok($"Vehiculo cargado correctamente. Codigo: {vehiculo.Codigo}");
        }
        catch (Exception e)
        {
            return BadRequest($"Error: {e.Message}");
        }
    }


    [HttpPut("CodigoAModificar/{codigo}")]
    public async Task<ActionResult> Put(string codigo, [FromBody] PUT_VehiculoDTO entidadDTO)
    {
		if (!await repositorio.ExisteByCodigo(codigo))
		{
			return BadRequest($"No se encontró un vehiculo con el código {codigo}, compruebe el valor ingresado.");
		}

		var vehiculo = await repositorio.SelectByCod(codigo);

		if (vehiculo == null)
		{
			return NotFound($"No se encontró un vehiculo con el código {codigo}, compruebe el valor ingresado.");
		}

		mapper.Map(entidadDTO, vehiculo);

		// Log para verificar los valores actualizados en verde 
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine($"Vehiculo actualizado: {vehiculo.Id}, {vehiculo.Codigo}");
		Console.ResetColor();

		try
		{
			await repositorio.Update(vehiculo.Id, vehiculo);
			var vehiculoDTO = mapper.Map<GET_VehiculoDTO>(vehiculo);
			return Ok(vehiculoDTO);
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


    [HttpDelete("EliminarCodigo/{codigo}")]
    public async Task<ActionResult> Delete(string codigo)
    {
		var existe = await repositorio.ExisteByCodigo(codigo);

		if (!existe)
		{
			return NotFound($"El vehiculo con código {codigo} no existe");
		}

		var EntidadABorrar = await repositorio.SelectByCod(codigo);

		if (EntidadABorrar == null)
		{
			return  NotFound($"No se encontró un vehiculo con código {codigo}. Favor verificar");
		}

		if (await repositorio.Delete(EntidadABorrar.Id))
		{
			return Ok($"El vehiculo con código {codigo} fue eliminada");
		}
		else
		{
			return BadRequest("No se pudo llevar a cabo la acción");
		}
	}
}
