﻿using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Repositorio.GinoRepositorios
{
    public class PlanVendidoRepositorio : Repositorio<PlanVendido>, IPlanVendidoRepositorio
    {
        private readonly Context context;

        public PlanVendidoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<PlanVendido>> SelectPlanYAsociados()
        {
            try
            {
                var planVendido = await context.PlanesVendidos.Include(pv => pv.Cliente).ThenInclude(c => c.Persona)
                    .Include(pv => pv.Vendedor).ThenInclude(v => v.Persona)
                    .Include(pv => pv.TipoPlan)
                    .Include(pv => pv.Adjudicacion).ThenInclude(a => a.Vehiculo)
                    .ToListAsync();

                return planVendido;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
        }

        public async Task<PlanVendido> SelectPlanYAsociadosById(int id)
        {

            try
            {
                var planVendidoById = await context.PlanesVendidos.Include(pv => pv.Cliente).ThenInclude(c => c.Persona)
                    .Include(pv => pv.Vendedor).ThenInclude(v => v.Persona)
                    .Include(pv => pv.TipoPlan)
                    .Include(pv => pv.Adjudicacion).ThenInclude(a => a.Vehiculo)
                    .FirstOrDefaultAsync(pv => pv.Id == id);

                return planVendidoById;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
        }
    }
}
