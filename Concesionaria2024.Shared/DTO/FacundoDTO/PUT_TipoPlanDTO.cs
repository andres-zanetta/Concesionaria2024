using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.FacundoDTO
{
    public class PUT_TipoPlanDTO
    {
        [Required(ErrorMessage = "El nombre del plan es obligatorio.")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? NombrePlan { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La cantidad de cuotas es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de cuotas debe ser un valor positivo")]
        public int CantCuotas { get; set; }

        [Required(ErrorMessage = "El valor total del plan es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "El Vehiculo es obligatorio.")]
        public int VehiculoId { get; set; }
    }

}
