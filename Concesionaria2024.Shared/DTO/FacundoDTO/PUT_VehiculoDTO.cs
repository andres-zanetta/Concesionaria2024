using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.FacundoDTO
{
    public class PUT_VehiculoDTO
    {
        [Required(ErrorMessage = "El ID es obligatorio.")]
        public int Id { get; set; }

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