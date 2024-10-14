using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.BrunoDTO
{
        public class PUT_CuotaDTO
        {
            // Número de Cuota ------------------------------------------------------------------------------------------

            [Required(ErrorMessage = "El número de cuota es obligatorio.")]
            public int NumeroCuota { get; set; }

            // Valor de la Cuota ------------------------------------------------------------------------------------------

            [Required(ErrorMessage = "El valor de la cuota es obligatorio.")]
            public decimal ValorCuota { get; set; }

            // Fecha de Vencimiento ------------------------------------------------------------------------------------------

            [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
            public DateTime FechaVencimiento { get; set; }

            // Estado ------------------------------------------------------------------------------------------

            [Required(ErrorMessage = "El estado de la cuota es obligatorio.")]
            public string Estado { get; set; }

            // Observaciones (opcional) ------------------------------------------------------------------------------------------

            [MaxLength(500, ErrorMessage = "Máximo número de caracteres {1}.")]
            public string? Observaciones { get; set; }

            // Plan Vendido ID ------------------------------------------------------------------------------------------

            [Required(ErrorMessage = "El ID del plan vendido es obligatorio.")]
            public int PlanVendidoId { get; set; }

            // Cuota Vencida ------------------------------------------------------------------------------------------

            [Required(ErrorMessage = "Debe especificar si la cuota está vencida o no.")]
            public bool CuotaVencida { get; set; }
        }
}

