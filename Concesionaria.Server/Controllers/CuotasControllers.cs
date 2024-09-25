using AutoMapper;
using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Concesionaria2024.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concesionaria.Server
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuotasControllers : ControllerBase
    {
        private readonly IRepositorio<Cuota> repositorio;
        private readonly IMapper mapper;

        public CuotasControllers(IRepositorio<Cuota> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }
        private readonly Context context;

        public CuotasControllers(Context context)
        {
            context = context;
        }
        //GET:---------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<List<Cuota>>> Get()
        {
            return await context.Cuotas.ToListAsync();
        }

        [HttpGet("{ID:int}")]
        public async Task<ActionResult<Cuota>> Get(int ID)
        {
            var cuota = await context.Cuotas.FirstOrDefaultAsync(x => x.Id == ID);

            if (cuota == null)
            {
                return NotFound($"La cuota con el ID: {ID} no fue encontrada");
            }
            return cuota;
        }


        //POST:------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<int>> Post(Cuota entidad)
        {
            try
            {
                context.Cuotas.Add(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearCuotaDTO entidadDTO)
        {
            try
            {
                Cuota entidad = mapper.Map<Cuota>(entidadDTO);
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

        //PUT: ----------------------------------------------------------------------
        [HttpPut("{ID:int}")]
        public async Task<ActionResult> Put(int ID, [FromBody] Cuota cuota)
        {
            if (ID != cuota.Id)
            {
                return BadRequest("El ID de la cuota no coincide.");
            }

            var cuotaExistente = await context.Cuotas.FirstOrDefaultAsync(x => x.Id == ID);
            if (cuotaExistente == null)
            {
                return NotFound($"No se encontró la cuota con ID {ID}.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            cuotaExistente.ValorCuota = cuota.ValorCuota;
            cuotaExistente.NumeroCuota = cuota.NumeroCuota;
            cuotaExistente.Estado = cuota.Estado;
            cuotaExistente.FechaVencimiento = cuota.FechaVencimiento;
            cuotaExistente.CuotaVencida = cuota.CuotaVencida;
            cuotaExistente.Observaciones = cuota.Observaciones;
            cuotaExistente.PlanVendidoId = cuota.PlanVendidoId;

            try
            {
                context.Cuotas.Update(cuotaExistente);
                await context.SaveChangesAsync();
                return Ok($"La cuota con ID {ID} fue actualizada correctamente.");
            }
            catch (DbUpdateException e)
            {
                return BadRequest($"Error al actualizar la cuota: {e.Message}");
            }
        }
        //DELETE: ------------------------------------------------------------
        [HttpDelete("{ID:int}")]
        public async Task<ActionResult> Delete(int ID)
        {
            var cuotaExistente = await context.Cuotas.FindAsync(ID);
            if (cuotaExistente == null)
            {
                return NotFound($"La cuota con ID {ID} no fue encontrada.");
            }

            try
            {
                context.Cuotas.Remove(cuotaExistente);
                await context.SaveChangesAsync();
                return Ok($"La cuota con ID {ID} fue eliminada correctamente.");
            }
            catch (DbUpdateException e)
            {
                return BadRequest($"Error al eliminar la cuota: {e.Message}");
            }
        }
    }
}



