using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concesionaria2024.Shared.DTO.FacundoDTO
{
    public class CrearVehiculoDTO
    {
        [Required(ErrorMessage = "La marca del Vehículo es obligatoria.")]
        [MaxLength(15, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El modelo del Vehículo es obligatorio.")]
        [MaxLength(15, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El precio del Vehículo es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")] 
        public decimal Precio { get; set; }

        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Motor { get; set; }

        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Combustible { get; set; }
    }
}
