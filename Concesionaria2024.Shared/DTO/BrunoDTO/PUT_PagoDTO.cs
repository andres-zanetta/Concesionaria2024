﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.BrunoDTO
{
    public class PUT_PagoDTO
    {
        // Valor del Pago ------------------------------------------------------------------------------------------

        [Required(ErrorMessage = "El valor del pago es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor del pago debe ser mayor a 0.")]
        public decimal ValorPago { get; set; }

        // Fecha del Pago ------------------------------------------------------------------------------------------

        [Required(ErrorMessage = "La fecha del pago es obligatoria.")]
        public DateTime FechaPago { get; set; }

        // Cuota ID ------------------------------------------------------------------------------------------

        [Required(ErrorMessage = "El ID de la cuota es obligatorio.")]
        public int CuotaId { get; set; }

        // Observaciones (opcional) ------------------------------------------------------------------------------------------

        [MaxLength(500, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Observaciones { get; set; }
    }
}
