using Concesionaria.DB.Data.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.GinoDTO
{
    public class PUT_PlanVendidoDTO
    {
        // Fecha cuando se suscribió ------------------------------------------------------------------------------------------


        public DateTime? FechaSuscripcion { get; set; }


        // Fecha del sorteo ------------------------------------------------------------------------------------------


        public DateTime? FechaSorteo { get; set; }


        // Descripción ------------------------------------------------------------------------------------------



        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Descripcion { get; set; }


        // Estado ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El estado del plan es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Estado { get; set; }


        // Fecha inicio ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }


        // Fecha fin ------------------------------------------------------------------------------------------


        //Agregar metodo estatico para que la fecha fin no sea anterior a la fecha inicio
        public DateTime? FechaFin { get; set; }


        // Plan en mora ------------------------------------------------------------------------------------------


        [Display(Name = "¿El plan tiene mora?", Description = "Indica si el plan tiene mora")]
        public bool PlanEnMora { get; set; }


       // ==================================== Ids de las entidades asociadas ==================================================


        [Required(ErrorMessage = "El tipo de plan es obligatorio.")]
        public int TipoPlanId { get; set; }


        [Required(ErrorMessage = "El vendedor es obligatorio.")]
        public int VendedorId { get; set; }


        [Required(ErrorMessage = "El cliente es obligatorio.")]
        public int ClienteId { get; set; }


        public int? AdjudicacionId { get; set; }

    }
}
