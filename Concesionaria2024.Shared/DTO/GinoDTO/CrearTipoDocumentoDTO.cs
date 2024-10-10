using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.GinoDTO
{
    public class CrearTipoDocumentoDTO
    {
        // Nombre ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El nombre del tipo de documento es obligatorio.")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }


        // Codigo ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El código del tipo de documento es obligatorio.")]
        [MaxLength(4, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Codigo { get; set; }
    }
}
