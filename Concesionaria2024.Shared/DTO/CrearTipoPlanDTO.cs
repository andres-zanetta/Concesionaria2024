﻿namespace Concesionaria2024.Shared.DTO
{
    public class CrearTipoPlanDTO
    {
        public string NombrePlan { get; set; }
        public string? Descripcion { get; set; }
        public int CantCuotas { get; set; }
        public decimal ValorTotal { get; set; }
        public int VehiculoId { get; set; }
    }
}
