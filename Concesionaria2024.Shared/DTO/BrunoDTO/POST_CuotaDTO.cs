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
        [Required(ErrorMessage = "El monto de la cuota es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorCuota { get; set; }


        // Número de la cuota ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La Capacidad maxima del vehiculo es obligatoria.")]
        public int NumeroCuota { get; set; }


        // Fcha de inicio ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        public DateTime FechaInicio { get; set; }


        // Fecha de vencimiento ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        public DateTime FechaVencimiento { get; set; }


        // Cuota vencida ------------------------------------------------------------------------------------------


        [Display(Name = "¿Está vencida?", Description = "Indica si la cuota está vencida")]
        //Para mostrar info en la interfaz de usuario
        public bool CuotaVencida { get; set; }


        // Observaciones ------------------------------------------------------------------------------------------


        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        // No se agrega un parametro "Required" dado que debe poder ser nullable
        public string? Observaciones { get; set; }


        public string CodigoPlanVendido { get; set; }
    }
}
