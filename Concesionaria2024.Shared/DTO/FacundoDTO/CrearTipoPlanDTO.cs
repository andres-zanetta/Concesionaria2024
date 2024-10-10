namespace Concesionaria2024.Shared.DTO.FacundoDTO
{
    public class CrearTipoPlanDTO
    {
        public string NombrePlan { get; set; }
        public string? Descripcion { get; set; }
        public int CantCuotas { get; set; }
        public decimal ValorTotal { get; set; }
        public int VehiculoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public decimal TasaInteres { get; set; }
        public string Moneda { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }
    }
}