using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concesionaria.Server.Controllers.FacundoControllers
{
    [ApiController]
    [Route("api/Adjudicacion")]
    public class AdjudicacionController : ControllerBase
    {
        private readonly Context context;

        public AdjudicacionController(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adjudicacion>>> GetAdjudicaciones()
        {
            return await context.Adjudicaciones
                                 .Include(a => a.Vehiculo)
                                 .Include(a => a.PlanVendido)
                                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Adjudicacion>> GetAdjudicacion(int id)
        {
            var adjudicacion = await context.Adjudicaciones
                                               .Include(a => a.Vehiculo)
                                               .Include(a => a.PlanVendido)
                                               .FirstOrDefaultAsync(a => a.Id == id);

            if (adjudicacion == null)
            {
                return NotFound();
            }

            return adjudicacion;
        }

        [HttpPost]
        public async Task<ActionResult<Adjudicacion>> PostAdjudicacion(Adjudicacion adjudicacion)
        {
            context.Adjudicaciones.Add(adjudicacion);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdjudicacion), new { id = adjudicacion.Id }, adjudicacion);
        }

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
                if (!AdjudicacionExiste(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

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

        private bool AdjudicacionExiste(int id)
        {
            return context.Adjudicaciones.Any(e => e.Id == id);
        }
    }
}
