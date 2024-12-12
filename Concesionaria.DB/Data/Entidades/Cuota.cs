using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria.DB.Data.Entidades
{
    public class Cuota : EntityBase
    {
        // Valor de la cuota ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El monto de la cuota es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorCuota { get; set; }


        // Número de la cuota ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La Capacidad maxima del vehiculo es obligatoria.")]
        //Esto debe ser autoincremental Consultar con el profesor si se puede arreglar via codigo

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //solo una columna por tabla puede tener un comportamiento Identity,
        //por lo que solo el Id se mantiene como autoincremental y NumeroCuota como un campo normal. 
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


        // Clase de navegación ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El plan vendido es obligatorio.")]
        public int PlanVendidoId { get; set; }
        public PlanVendido PlanVendido { get; set; }


        public Pago Pago { get; set; } 
    }
}
