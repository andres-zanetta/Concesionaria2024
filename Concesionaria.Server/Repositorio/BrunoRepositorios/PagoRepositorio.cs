using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Repositorio.BrunoRepositorios
{
    public class PagoRepositorio : Repositorio<Pago>
    {
        private readonly Context context;

        public PagoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        // Muestra un pago específico y la información de la cuota asociada
        public async Task<Pago> SelectPagoConCuotaId(int id)
        {
            try
            {
                var pago = await context.Pagos.Include(p => p.Cuota)
                                              .FirstOrDefaultAsync(p => p.Id == id);
                return pago;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener el pago: {e.Message}");
                throw;
            }
        }

        // Muestra todos los pagos asociados a una cuota en particular
        public async Task<List<Pago>> SelectPagosXCuotaId(int cuotaId)
        {
            try
            {
                var pagos = await context.Pagos.Where(p => p.CuotaId == cuotaId).ToListAsync();
                return pagos;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener los pagos por cuota: {e.Message}");
                throw;
            }
        }

    }
}

