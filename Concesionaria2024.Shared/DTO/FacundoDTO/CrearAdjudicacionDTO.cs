using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.FacundoDTO
{
    public class CrearAdjudicacionDTO
    {
        public int Id { get; set; }
        public string? Detalle { get; set; }
        public DateTime FechaAdjudicacion { get; set; }
        public bool AutoEntregado { get; set; }
        public string PatenteVehiculo { get; set; } = string.Empty;
        public int VehiculoId { get; set; }
        public int PlanVendidoId { get; set; }
    }
}
