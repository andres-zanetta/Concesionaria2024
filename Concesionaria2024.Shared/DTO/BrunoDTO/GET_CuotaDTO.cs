using Concesionaria.DB.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.BrunoDTO
{
    public class GET_CuotaDTO : EntityBase
    {
        // Número de Cuota ------------------------------------------------------------------------------------------

        [Required(ErrorMessage = "El número de cuota es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de cuota debe ser mayor a 0.")]
        public int NumeroCuota { get; set; }

        // Valor de la Cuota ------------------------------------------------------------------------------------------

        [Required(ErrorMessage = "El valor de la cuota es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor de la cuota debe ser mayor a 0.")]
        public decimal ValorCuota { get; set; }

        // Fecha de Vencimiento ------------------------------------------------------------------------------------------

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        public DateTime FechaVencimiento { get; set; }

        // Estado ------------------------------------------------------------------------------------------

        [Required(ErrorMessage = "El estado de la cuota es obligatorio.")]
        public string Estado { get; set; }

        // Observaciones ------------------------------------------------------------------------------------------

        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Observaciones { get; set; }

        // Plan Vendido ID ------------------------------------------------------------------------------------------

        [Required(ErrorMessage = "El ID del plan vendido es obligatorio.")]
        public int PlanVendidoId { get; set; }

        // Cuota Vencida ------------------------------------------------------------------------------------------

        [Display(Name = "¿La cuota está vencida?")]
        public bool CuotaVencida { get; set; }
    }
}
