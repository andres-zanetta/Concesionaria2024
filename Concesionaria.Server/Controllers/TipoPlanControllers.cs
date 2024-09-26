using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using System.Threading.Tasks;
using System.Linq;

namespace Concesionaria.Controllers
{
    [ApiController]
    [Route("api/TipoPlan")]
    public class TipoPlanController : ControllerBase
    {
        private readonly Context context;

        public TipoPlanController(Context context)
        {
            context = context;
        }
        //Obtener todos los tipos de planes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoPlan>>> GetTipoPlanes()
        {
            return await context.TipoPlanes.Include(tp => tp.Vehiculo).ToListAsync();
        }
        //Obtiene id tipo de plan
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoPlan>> GetTipoPlan(int id)
        {
            var tipoPlan = await context.TipoPlanes.Include(tp => tp.Vehiculo).MinAsync();

            if (tipoPlan == null)
            {
                return NotFound();
            }

            return tipoPlan;
        }
        //crear nuevo tipo
        [HttpPost]
        public async Task<ActionResult<TipoPlan>> PostTipoPlan(TipoPlan tipoPlan)
        {
            context.TipoPlanes.Add(tipoPlan);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetTipoPlan", new { id = tipoPlan.Id }, tipoPlan);
        }
        //Actualizar tipo existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoPlan(int id, TipoPlan tipoPlan)
        {
            if (id != tipoPlan.Id)
            {
                return BadRequest();
            }

            context.Entry(tipoPlan).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoPlanExists(id))
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
        //Eliminar tipo
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoPlan(int id)
        {
            var tipoPlan = await context.TipoPlanes.FindAsync(id);
            if (tipoPlan == null)
            {
                return NotFound();
            }

            context.TipoPlanes.Remove(tipoPlan);
            await context.SaveChangesAsync();

            return NoContent();
        }
        //Verificador de existentes
        private bool TipoPlanExists(int id)
        {
            return context.TipoPlanes.Any(e => e.Id == id);
        }
    }
}
