namespace Concesionaria2024.Shared.DTO.FacundoDTO.Adjudicacion
{
    public class POST_AdjudicacionDTO
    {
        public string? Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string? Estado { get; set; }
        public object Codigo { get; set; }
        public object PatenteVehiculo { get; set; }
        public object VehiculoId { get; set; }
    }
}

