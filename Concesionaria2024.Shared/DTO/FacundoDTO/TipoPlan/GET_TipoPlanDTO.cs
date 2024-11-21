namespace Concesionaria2024.Shared.DTO.FacundoDTO.TipoPlan
{
    public class GET_TipoPlanDTO
    {
        public int Id { get; set; } 
        public string? NombrePlan { get; set; }
        public string? Descripcion { get; set; }
        public int CantCuotas { get; set; }
        public decimal ValorTotal { get; set; }
        public int VehiculoId { get; set; }

        public string? NombreVehiculo { get; set; } 
    }
}
