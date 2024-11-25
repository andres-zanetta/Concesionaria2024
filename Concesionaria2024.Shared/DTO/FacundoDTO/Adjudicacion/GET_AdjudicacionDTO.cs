namespace Concesionaria2024.Shared.DTO.FacundoDTO.Adjudicacion
{
    public class GET_AdjudicacionDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string? Estado { get; set; }
        public object PatenteVehiculo { get; set; }
    }
}

