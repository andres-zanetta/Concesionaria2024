using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.BrunoDTO
{
    public class POST_PagoDTO
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


        //// Valor del Pago ------------------------------------------------------------------------------------------

        //[Required(ErrorMessage = "El valor del pago es obligatorio.")]
        //[Range(0.01, double.MaxValue, ErrorMessage = "El valor del pago debe ser mayor a 0.")]
        //public decimal ValorPago { get; set; }

        //// Fecha del Pago ------------------------------------------------------------------------------------------

        //[Required(ErrorMessage = "La fecha del pago es obligatoria.")]
        //public DateTime FechaPago { get; set; }

        //// Cuota ID ------------------------------------------------------------------------------------------

        //[Required(ErrorMessage = "El ID de la cuota es obligatorio.")]
        //public int CuotaId { get; set; }

        //// Observaciones (opcional) ------------------------------------------------------------------------------------------

        //[MaxLength(500, ErrorMessage = "Máximo número de caracteres {1}.")]
        //public string? Observaciones { get; set; }
    }
}
