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
                    .Include(pv => pv.TipoPlan).ThenInclude(a => a.Vehiculo)
                    .Include(pv => pv.Adjudicacion)
                    .ToListAsync();

                return planVendido;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
        }

        public async Task<PlanVendido> SelectByIdCliente(int id)
        {
            try
            {
                var planVendido = await context.PlanesVendidos.Include(pv => pv.Cliente).ThenInclude(c => c.Persona)
                    .Include(pv => pv.Vendedor).ThenInclude(v => v.Persona)
                    .Include(pv => pv.TipoPlan).ThenInclude(a => a.Vehiculo)
                    .Include(pv => pv.Adjudicacion)
                    .FirstOrDefaultAsync(pv => pv.Cliente.Id == id);
                return planVendido;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
        }


        public async Task<PlanVendido> SelectPlanYAsociadosByCodigo(string codigo)
        {

            try
            {
                var planVendidoByCodigo = await context.PlanesVendidos.Include(pv => pv.Cliente).ThenInclude(c => c.Persona)
                    .Include(pv => pv.Vendedor).ThenInclude(v => v.Persona)
                    .Include(pv => pv.TipoPlan).ThenInclude(a => a.Vehiculo)
                    .Include(pv => pv.Adjudicacion)
                    .FirstOrDefaultAsync(pv => pv.Codigo == codigo);

                return planVendidoByCodigo;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
        }
    }
}

