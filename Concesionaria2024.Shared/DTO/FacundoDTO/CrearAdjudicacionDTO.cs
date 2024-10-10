namespace Concesionaria2024.Shared.DTO.FacundoDTO;

public class CrearAdjudicacionDTO
{
    public string? Detalle { get; set; }
    public DateTime FechaAdjudicacion { get; set; }
    public bool AutoEntregado { get; set; }
    public string PatenteVehiculo { get; set; }
    public int VehiculoId { get; set; }
    public int PlanVendidoId { get; set; }
}
