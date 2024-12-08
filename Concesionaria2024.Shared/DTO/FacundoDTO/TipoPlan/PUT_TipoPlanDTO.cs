using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concesionaria2024.Shared.DTO.FacundoDTO.TipoPlan
{
    public class PUT_TipoPlanDTO
    {
        [Required(ErrorMessage = "El nombre del plan es obligatorio.")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? NombrePlan { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La cantidad de cuotas es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de cuotas debe ser un valor positivo.")]
        public int CantCuotas { get; set; }

        [Required(ErrorMessage = "El valor total del plan es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor total del plan debe ser mayor a cero.")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "El Vehículo es obligatorio.")]
        public string CodigoVehiculo { get; set; }
    }

}
