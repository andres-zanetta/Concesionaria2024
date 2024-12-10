using Concesionaria.DB.Data.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concesionaria.Server.Repositorio.FacundoRepositorios
{
    public interface IAdjudicacionRepositorio : IRepositorio<Adjudicacion>
    {
        Task<Adjudicacion> SelectByCodigoConVehiculo(string codigo);
        Task<Adjudicacion> SelectByPatente(string patente);
        Task<List<Adjudicacion>> SelectEntidadConVehiculo();
        Task<Adjudicacion> SelectEntidadConVehiculoById(int id);
    }
}

