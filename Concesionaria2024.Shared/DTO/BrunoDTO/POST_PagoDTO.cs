using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.BrunoDTO
{
    public class POST_PagoDTO
    {

        // Fecha pago ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La fecha del pago es obligatoria.")]
        public DateTime FechaPago { get; set; }


        // Metodo de pago ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El metodo de pago es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string MetodoPago { get; set; }


        // REferencia del pago ------------------------------------------------------------------------------------------


        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? ReferenciaPago { get; set; }


        [Required(ErrorMessage = "El metodo de pago es obligatorio.")]
        public string CodigoCuota { get; set; }
    }
}
