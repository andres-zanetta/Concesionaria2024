using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Concesionaria.Server.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.Server.Repositorio.FacundoRepositorios
{
    public class AdjudicacionRepositorio : Repositorio<Adjudicacion>, IAdjudicacionRepositorio
    {
        private readonly Context context;

        public AdjudicacionRepositorio(Context context) : base(context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Adjudicacion>> GetByVehiculoIdAsync(int vehiculoId)
        {
            return await context.Set<Adjudicacion>()
                .Where(a => a.VehiculoId == vehiculoId)
                .ToListAsync();
        }

        public async Task<List<Adjudicacion>> GetByFechaAdjudicacionRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await context.Set<Adjudicacion>()
                .Where(a => a.FechaAdjudicacion >= startDate && a.FechaAdjudicacion <= endDate)
                .ToListAsync();
        }

        public async Task<List<Adjudicacion>> GetEntregadosAsync()
        {
            return await context.Set<Adjudicacion>()
                .Where(a => a.AutoEntregado)
                .ToListAsync();
        }

        public async Task<Adjudicacion?> GetByPatenteAsync(string patente)
        {
            return await context.Set<Adjudicacion>()
                .FirstOrDefaultAsync(a => a.PatenteVehiculo == patente);
        }

        public Task GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
