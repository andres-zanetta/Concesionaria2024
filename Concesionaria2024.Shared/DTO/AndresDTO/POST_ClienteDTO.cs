using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concesionaria2024.Shared.DTO.AndresDTO
{
    public class POST_ClienteDTO:EntityBase
    {
        // Fecha ------------------------------------------------------------------------------------------
        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        public DateTime FechaInicio { get; set; }


        // Clase de navegación ------------------------------------------------------------------------------------------

        [Required(ErrorMessage = "La persona es obligatoria.")]
        public int PersonaId { get; set; }

    }
}
