using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.GinoDTO.PlanVendido
{
    public class POST_PlanVendidoDNI_DTO
    {

        // Descripción ----------------------------------------------------------------------------


        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Descripcion { get; set; }


        // Estado ---------------------------------------------------------------------------------


        [Required(ErrorMessage = "El estado del plan es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Estado { get; set; }



        //NavClass---------------------------------------------------------------------------------


        [Required(ErrorMessage = "El venderdor es obligatoria.")]
        public string VendedorDNI { get; set; }


        [Required(ErrorMessage = "El clienteo es obligatoria.")]
        public string ClienteDNI { get; set; }


        [Required(ErrorMessage = "El tipo de plan es obligatoria.")]
        public string TipoPlanCodigo { get; set; }

    }
}
