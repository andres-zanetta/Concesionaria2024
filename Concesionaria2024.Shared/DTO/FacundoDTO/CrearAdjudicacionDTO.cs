using System;

namespace Concesionaria.DB.Data.Dtos
{
    public class AdjudicacionDto
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
