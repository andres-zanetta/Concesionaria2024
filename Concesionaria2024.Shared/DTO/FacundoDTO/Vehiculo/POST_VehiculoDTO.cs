using System.ComponentModel.DataAnnotations;

namespace Concesionaria2024.Shared.DTO.FacundoDTO.Vehiculo
{
    public class POST_VehiculoDTO
    {
        [Required(ErrorMessage = "El codigo del Vehiculo es obligatoria.")]
        [MaxLength(30, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
        public decimal Precio { get; set; }
        public string? Motor { get; set; }
        public string? Combustible { get; set; }
    }
}
