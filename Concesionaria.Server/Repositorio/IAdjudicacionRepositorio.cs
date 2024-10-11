using Concesionaria.DB.Data.Entidades;
using Concesionaria.DB.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Concesionaria.Server.Repositorio;

namespace Concesionaria.DB.Data.Repositorios
{
    public interface IAdjudicacionRepositorio : IRepositorio<Adjudicacion>
    {
        Task<List<Adjudicacion>> GetByVehiculoIdAsync(int vehiculoId);
        Task<List<Adjudicacion>> GetByFechaAdjudicacionRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<Adjudicacion>> GetEntregadosAsync();
        Task<Adjudicacion?> GetByPatenteAsync(string patente);
        Task GetAllAsync();
    }
}