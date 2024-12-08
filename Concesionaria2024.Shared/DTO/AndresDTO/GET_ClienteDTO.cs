using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.AndresDTO
{
    public class GET_ClienteDTO
    {
        [Required(ErrorMessage = "El codigo es obligatorio.")]
        [MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Codigo { get; set; }


        // Fecha ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        public DateTime FechaInicio { get; set; }


        public DateTime? FechaFin { get; set; }


		public string NombrePersona { get; set; }


		public string NumDoc { get; set; }
    }
}
