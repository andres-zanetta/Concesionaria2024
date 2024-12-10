using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concesionaria.Server.Repositorio.FacundoRepositorios
{
    public class AdjudicacionRepositorio : Repositorio<Adjudicacion>, IAdjudicacionRepositorio
    {
        private readonly Context context;

        public AdjudicacionRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Adjudicacion> SelectByCodigoConVehiculo(string codigo)
        {
            try
            {
                var adjudicacion = await context.Adjudicaciones.Include(a => a.PlanVendido)
                    .ThenInclude(pv => pv.TipoPlan).ThenInclude(tp => tp.Vehiculo).FirstOrDefaultAsync(a => a.Codigo == codigo);
                return adjudicacion;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros:  {e.Message}");
                throw e;
            }

        }

        public async Task<List<Adjudicacion>> SelectEntidadConVehiculo()
        {
            try
            {
                var adjudicaciones = await context.Adjudicaciones.Include(a => a.PlanVendido)
                    .ThenInclude(pv => pv.TipoPlan).ThenInclude(tp => tp.Vehiculo).ToListAsync();
                return adjudicaciones;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
        }

        public async Task<Adjudicacion> SelectByPatente(string patente)
        {
            try
            {
                var adjudicacion = await context.Adjudicaciones.Include(a => a.PlanVendido)
                    .ThenInclude(pv => pv.TipoPlan).ThenInclude(tp => tp.Vehiculo).FirstOrDefaultAsync(a => a.PatenteVehiculo == patente);
                return adjudicacion;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
        }
        public async Task<Adjudicacion> SelectEntidadConVehiculoById(int id)
        {
            try
            {
                var adjudicacion = await context.Adjudicaciones.Include(a => a.PlanVendido).FirstOrDefaultAsync(a => a.Id == id);
                return adjudicacion;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
        }
    }
}
