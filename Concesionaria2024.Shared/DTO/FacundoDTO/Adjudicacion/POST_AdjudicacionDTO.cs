using System.ComponentModel.DataAnnotations;

namespace Concesionaria2024.Shared.DTO.FacundoDTO.Adjudicacion
{
    public class POST_AdjudicacionDTO
    {
        // Detalle ------------------------------------------------------------------------------------------


        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        // No se agrega un parametro "Required" dado que debe poder ser nullable
        public string? Detalle { get; set; }


        // Fecha de adjudicación ----------------------------------------------------------------------------


        //[Required(ErrorMessage = "La fecha de Adjudicación es obligatoria.")]
        //public DateTime FechaAdjudicacion { get; set; }


        // Auto entregado  ----------------------------------------------------------------------------------


        [Display(Name = "¿Está entregado?", Description = "Indica si el vehiculo fue entregado")]
        //Para mostrar info en la interfaz de usuario
        public bool AutoEntregado { get; set; }


        // Patente del vehiculo -----------------------------------------------------------------------------


        [Required(ErrorMessage = "La patente del vehiculo es obligatoria.")]
        [MaxLength(10, ErrorMessage = "Máximo número de caracteres: {1}.")]
        public string PatenteVehiculo { get; set; }

        // Codigo del Vehiculo ------------------------------------------------------------------------------

        [Required(ErrorMessage = "El Codigo del vehiculo es obligatorio.")]
        [MaxLength(50, ErrorMessage = "Máximo número de caracteres: {1}.")]
        public string PlanVendidoCodigo { get; set; }
    }
}

