using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.AndresDTO
{
    public class POST_ClienteSinFechaInicio
    {
        // Clase de navegación ------------------------------------------------------------------------------------------

        [Required(ErrorMessage = "La persona es obligatoria.")]
        public int PersonaId { get; set; }

    }
}
