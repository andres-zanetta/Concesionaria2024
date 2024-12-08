using Concesionaria.DB.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.AndresDTO
{
    public class PUT_VendedorDTO
    {
		// Fecha ------------------------------------------------------------------------------------------
		[Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
		public DateTime FechaInicio { get; set; }

		public DateTime? FechaFin { get; set; }


		[Required(ErrorMessage = "El número de documento es obligatorio.")]
		[MaxLength(15, ErrorMessage = "Máximo número de caracteres {1}.")]
		public string NumDoc { get; set; }

	}
}
