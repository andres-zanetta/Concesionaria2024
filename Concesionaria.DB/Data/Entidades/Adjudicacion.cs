using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria.DB.Data.Entidades
{
    [Index(nameof(PatenteVehiculo), Name = "Adjudicacion_UQ", IsUnique = true)]
    public class Adjudicacion : EntityBase
    {
        // Detalle ------------------------------------------------------------------------------------------


        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        // No se agrega un parametro "Required" dado que debe poder ser nullable
        public string? Detalle { get; set; }


        // Fecha de adjudicación ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        public DateTime FechaAdjudicacion { get; set; }


        // Auto entregado  ------------------------------------------------------------------------------------------


        [Display(Name = "¿Está entregado?", Description = "Indica si el vehiculo fue entregado")]
        //Para mostrar info en la interfaz de usuario
        public bool AutoEntregado { get; set; }


        // Patente del vehiculo ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La patente del vehiculo es obligatorio.")]
        [MaxLength(10, ErrorMessage = "Máximo número de caracteres: {1}.")]
        public string PatenteVehiculo { get; set; }



        // Clases de navegación ------------------------------------------------------------------------------------------



        [Required(ErrorMessage = "El vehiculo es obligatorio.")]
        public int VehiculoId { get; set; }
        public Vehiculo Vehiculo { get; set; }
       
    }
}
