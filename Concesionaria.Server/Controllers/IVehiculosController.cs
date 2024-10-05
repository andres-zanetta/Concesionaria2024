using Concesionaria.DB.Data.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.Server.Controllers
{
    public interface IVehiculosController
    {
        Task<IActionResult> DeleteVehiculo(int id);
        Task<ActionResult<Vehiculo>> GetVehiculo(int id);
        Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculos();
        Task<ActionResult<Vehiculo>> PostVehiculo(Vehiculo vehiculo);
        Task<IActionResult> PutVehiculo(int id, Vehiculo vehiculo);
    }
}