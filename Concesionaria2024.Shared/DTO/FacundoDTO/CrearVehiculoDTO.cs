using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concesionaria2024.Shared.DTO.FacundoDTO
{
    public class CrearVehiculoDTO
    {
        [Required(ErrorMessage = "La marca es obligatoria.")]
        public string? Marca { get; set; }

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        public string? Modelo { get; set; }

        [Required(ErrorMessage = "El año es obligatorio.")]
        [Range(1886, 2100, ErrorMessage = "Año no válido.")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "El color es obligatorio.")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
        public decimal Precio { get; set; }
    }
}
