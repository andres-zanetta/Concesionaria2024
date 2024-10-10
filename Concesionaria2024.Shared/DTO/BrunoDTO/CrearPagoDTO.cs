using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.BrunoDTO
{
    public class CrearPagoDTO
    {
        // Monto pagado ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El monto pagado de la cuota es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MontoPagado { get; set; }


        // Fecha pago ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La fecha del pago es obligatoria.")]
        public DateTime FechaPago { get; set; }


        // Metodo de pago ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El metodo de pago es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string MetodoPago { get; set; }


        // REferencia del pago ------------------------------------------------------------------------------------------


        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? ReferenciaPago { get; set; }
    }
}
