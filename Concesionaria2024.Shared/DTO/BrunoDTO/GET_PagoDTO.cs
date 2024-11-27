using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.BrunoDTO
{
    public class GET_PagoDTO
    {
        [Required(ErrorMessage = "El ID del pago es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El monto pagado es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto pagado debe ser mayor a 0.")]
        public decimal MontoPagado { get; set; }

        [Required(ErrorMessage = "La fecha del pago es obligatoria.")]
        public DateTime FechaPago { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string MetodoPago { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? ReferenciaPago { get; set; }

        [Required(ErrorMessage = "El ID de la cuota es obligatorio.")]
        public int CuotaId { get; set; }

        // Relación con Cuota
        public GET_CuotaDTO? Cuota { get; set; }

    }

    //// ID del Pago ------------------------------------------------------------------------------------------

    //[Required(ErrorMessage = "El ID del pago es obligatorio.")]
    //public int Id { get; set; }

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

