using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.BrunoDTO
{
    public class POST_CuotaDTO
    {
        // Número de Cuota ------------------------------------------------------------------------------------------

        [Required(ErrorMessage = "El numero de Cuota es obligatorio.")]
        //Esto debe ser autoincremental Consultar con el profesor si se puede arreglar via codigo
        public int NumeroCuota { get; set; }

        // Valor de la Cuota ------------------------------------------------------------------------------------------

        [Required(ErrorMessage = "El monto de la cuota es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorCuota { get; set; }

        // Fecha de Vencimiento ------------------------------------------------------------------------------------------

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        public DateTime FechaVencimiento { get; set; }


        // Observaciones (opcional) ------------------------------------------------------------------------------------------

        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        // No se agrega un parametro "Required" dado que debe poder ser nullable
        public string? Observaciones { get; set; }

        // Plan Vendido ID ------------------------------------------------------------------------------------------

        [Required(ErrorMessage = "El ID del plan vendido es obligatorio.")]
        public int PlanVendidoId { get; set; }

        // Cuota Vencida ------------------------------------------------------------------------------------------

        [Display(Name = "¿Está vencida?", Description = "Indica si la cuota está vencida")]
        //Para mostrar info en la interfaz de usuario
        public bool CuotaVencida { get; set; }
    }
}
