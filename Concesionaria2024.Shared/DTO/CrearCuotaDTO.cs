using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO
{
    public class CrearCuotaDTO
    {
        // Valor de la cuota ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El monto de la cuota es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorCuota { get; set; }


        // Número de la cuota ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La Capacidad maxima del vehiculo es obligatoria.")]
        //Esto debe ser autoincremental Consultar con el profesor si se puede arreglar via codigo
        public int NumeroCuota { get; set; }


        // Estado ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El estado de la cuota es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Estado { get; set; }


        // FEcha de vencimiento ------------------------------------------------------------------------------------------


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
    }
}
