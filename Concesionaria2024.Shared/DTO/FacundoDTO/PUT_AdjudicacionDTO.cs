using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.FacundoDTO
{
    public class PUT_AdjudicacionDTO
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Detalle { get; set; }

        [Required(ErrorMessage = "La fecha de adjudicación es obligatoria.")]
        public DateTime FechaAdjudicacion { get; set; }

        public bool AutoEntregado { get; set; }

        [Required(ErrorMessage = "La patente del vehículo es obligatoria.")]
        [MaxLength(10, ErrorMessage = "Máximo número de caracteres: {1}.")]
        public string? PatenteVehiculo { get; set; }

        [Required(ErrorMessage = "El ID del vehículo es obligatorio.")]
        public int VehiculoId { get; set; }
    }
}
