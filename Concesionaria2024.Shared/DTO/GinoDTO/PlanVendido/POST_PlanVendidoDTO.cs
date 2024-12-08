using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.GinoDTO.PlanVendido
{
    public class POST_PlanVendidoDTO
    {
        // Fecha cuando se suscribió --------------------------------------------------------------


        public DateTime FechaSuscripcion { get; set; }



        // Descripcion ----------------------------------------------------------------------------



        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Descripcion { get; set; }


        // Estado ---------------------------------------------------------------------------------


        [Required(ErrorMessage = "El estado del plan es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Estado { get; set; }


        // Fecha inicio ---------------------------------------------------------------------------


        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }


        //NavClass---------------------------------------------------------------------------------


        [Required(ErrorMessage = "El venderdor es obligatoria.")]
        public int VendedorId { get; set; }


        [Required(ErrorMessage = "El clienteo es obligatoria.")]
        public int ClienteId { get; set; }


        [Required(ErrorMessage = "El tipo de plan es obligatoria.")]
        public int TipoPlanId { get; set; }

    }
}
