using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using System.Threading.Tasks;

namespace Concesionaria.Controllers
{
    [ApiController]
    [Route("api/Adjudicacion")]
    public class AdjudicacionController : ControllerBase
    {
        private readonly Context context;

        public AdjudicacionController(Context context)
        {
            context = context;
        }

        //Metodo para obtener todas las adj al momento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adjudicacion>>> GetAdjudicaciones()
        {
            return await context.Adjudicaciones.Include(a => a.Vehiculo).Include(a => a.PlanVendido).ToListAsync();
        }

        //Adjudicacion especifica
        [HttpGet("{id}")]
        public async Task<ActionResult<Adjudicacion>> GetAdjudicacion(int id)
        {
            var adjudicacion = await context.Adjudicaciones.Include(a => a.Vehiculo).Include(a => a.PlanVendido).MinAsync();

            if (adjudicacion == null)
            {
                return NotFound();
            }

            return adjudicacion;
        }

        //Crear nueva aj
        [HttpPost]
        public async Task<ActionResult<Adjudicacion>> PostAdjudicacion(Adjudicacion adjudicacion)
        {
            context.Adjudicaciones.Add(adjudicacion);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdjudicacion), new { id = adjudicacion.Id }, adjudicacion);
        }

        //Actualizar una adj
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdjudicacion(int id, Adjudicacion adjudicacion)
        {
            if (id != adjudicacion.Id)
            {
                return BadRequest();
            }

            context.Entry(adjudicacion).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdjudicacionExists(id))
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

        //Eliminar adj
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdjudicacion(int id)
        {
            var adjudicacion = await context.Adjudicaciones.FindAsync(id);
            if (adjudicacion == null)
            {
                return NotFound();
            }

            context.Adjudicaciones.Remove(adjudicacion);
            await context.SaveChangesAsync();

            return NoContent();
        }
        //Comprobar existencia de adj
        private bool AdjudicacionExists(int id)
        {
            return context.Adjudicaciones.Any(e => e.Id == id);
        }
    }
}
