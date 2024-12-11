using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Concesionaria.Server.Repositorio.BrunoRepositorios
{
    public class CuotaRepositorio : Repositorio<Cuota>, ICuotaRepositorio
    {
        private readonly Context context;

        public CuotaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Cuota>> SelectByPVCodigoAll(string codigo)
        {
            try
            {
                var cuotas = await context.Cuotas.Include(c => c.Pagos).Include(c => c.PlanVendido).
                    Where(pv => pv.PlanVendido.Codigo == codigo).ToListAsync(); // Incluye la entidad Pago
                return cuotas;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
        }

        public async Task<bool> ExisteConPVIncluido(string codigo)
        {
            try
            {
                var ExisteEntidad = await context.Cuotas.Include(c => c.PlanVendido).AnyAsync(c => c.PlanVendido.Codigo == codigo);
                return ExisteEntidad;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }  
        }

        public async Task<List<Cuota>> SelectEntidadAll()
        {
            try
            {
                var cuotas = await context.Cuotas.Include(c => c.Pagos).Include(c => c.PlanVendido).ToListAsync(); // Incluye la entidad Pago
                return cuotas;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
        }


        public async Task<Cuota> SelectEntidadByCodConPagos(string codigo)
        {
            try
            {
                var cuota = await context.Cuotas.Include(c => c.Pagos).Include(c => c.PlanVendido).FirstOrDefaultAsync(c => c.Codigo == codigo); // Incluye la entidad Pago
                return cuota;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
        }

        public async Task<Cuota> SelectEntidadByNumCuotaConPagos(int numCuota)
        {
            try
            {
                var cuota = await context.Cuotas.Include(c => c.Pagos).Include(c => c.PlanVendido).FirstOrDefaultAsync(c => c.NumeroCuota == numCuota); 
                // Incluye la entidad Pago
                return cuota;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los registros: {e.Message}");
                throw;
            }
        }

        // generador de cuotas que voy a usar en el controller de PlanVendidoController
        public async Task Generarcuotas(int planVendidoId)
        {
            try
            {
                var planVendido = await context.PlanesVendidos
                   .Include(pv => pv.cuotas)
                   .Include(pv => pv.TipoPlan)
                   .FirstOrDefaultAsync(p => p.Id == planVendidoId);

                if (planVendido == null)
                {
                    throw new Exception($"No se encontró el PlanVendido con Id {planVendidoId}");
                }


                var listaCuota = new List<Cuota>(); 

                var fechaInicio = planVendido.FechaInicio.AddMonths(2).AddDays(1 - planVendido.FechaInicio.Day); 

                // la fecha es siempre el primero de 2 meses siguiente al inicio del plan.
                // para evitar que el que se suscriba un 29 tenga que pagar antes del 10 del mes siguiente
                for (int i = 0; i < planVendido.TipoPlan.CantCuotas; i++)
                {
                    var cuota = new Cuota
                    {
                        Codigo = $"{fechaInicio.ToString("dd-MM-yyyy")} - Cuota {i+1}",
                        FechaInicio = fechaInicio,
                        FechaVencimiento = fechaInicio.AddDays(10),
                        NumeroCuota = i + 1,
                        CuotaVencida = false,
                        PlanVendidoId = planVendidoId,
                        ValorCuota = (planVendido.TipoPlan.ValorTotal)*1.10m/planVendido.TipoPlan.CantCuotas
                    };

                    listaCuota.Add(cuota);
                    fechaInicio = fechaInicio.AddMonths(1);
                }
                planVendido.cuotas.AddRange(listaCuota);  // agrego la lista al plan
                await context.SaveChangesAsync();         // guardo cambios en la bd

               // return listaCuota;                        
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener las cuotas vencidas: {e.Message}");
                throw;
            }
        }


        // Muestra una cuota por ID con su plan vendido
        public async Task<Cuota> SelectCuotaConPlanVendidoXId(int id)
        {
            try
            {
                var cuota = await context.Cuotas.Include(c => c.PlanVendido)
                                                .FirstOrDefaultAsync(c => c.Id == id);
                return cuota;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener la cuota: {e.Message}");
                throw;
            }
        }

        // Muestra todas las cuotas vencidas
        public async Task<List<Cuota>> SelectCuotasVencidas()
        {
            try
            {
                var cuotas = await context.Cuotas.Where(c => c.CuotaVencida).ToListAsync();
                return cuotas;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener las cuotas vencidas: {e.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteByPVCod(string codigo)
        {
            var ListCuotasEliminar = await SelectByPVCodigoAll(codigo);

            if (ListCuotasEliminar == null || !ListCuotasEliminar.Any())
            {
                return false;
            }

            context.Cuotas.RemoveRange(ListCuotasEliminar);
            await context.SaveChangesAsync();
            return true;
        }
    }
}

