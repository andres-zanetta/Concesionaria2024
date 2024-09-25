using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using System.Threading.Tasks;

namespace Concesionaria.Server.Controllers
{
    [ApiController]
    [Route("api/Vehiculo")]
    public class VehiculosController : ControllerBase
    {
        private readonly Context context;

        public VehiculosController(Context context)
        {
            context = context;
        }
        //Obtener lista de vehiculos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculos()
        {
            return await context.Vehiculos.ToListAsync();
        }
        // Obtiene vehiculo por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehiculo>> GetVehiculo(int id)
        {
            var vehiculo = await context.Vehiculos.FindAsync(id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return vehiculo;
        }
        //Crear nuevo vehiculo
        [HttpPost]
        public async Task<ActionResult<Vehiculo>> PostVehiculo(Vehiculo vehiculo)
        {
            context.Vehiculos.Add(vehiculo);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetVehiculo", new { id = vehiculo.Id }, vehiculo);
        }
        //Acutalizar vehiculo existente
        //Comprueba coincidencia ID en URL
        //Marca objeto vehiculo modif y guarda cambio
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiculo(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Id)
            {
                return BadRequest();
            }

            context.Entry(vehiculo).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //Eliminar vehiculo... Delete
        //Si no encuentra ID devuelve 404 found
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculo(int id)
        {
            var vehiculo = await context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            context.Vehiculos.Remove(vehiculo);
            await context.SaveChangesAsync();

            return NoContent();
        }
        //Metodo aux: verifica si un vehículo existe en la base de datos por su ID... Any=booleano
        private bool VehiculoExists(int id)
        {
            return context.Vehiculos.Any(e => e.Id == id);
        }
    }
}
