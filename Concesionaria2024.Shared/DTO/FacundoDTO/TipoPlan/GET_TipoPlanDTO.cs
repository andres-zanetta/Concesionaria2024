using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concesionaria2024.Shared.DTO.FacundoDTO.TipoPlan
{
    public class GET_TipoPlanDTO
    {

		// Codigo  ------------------------------------------------------------------------------------------------

		[Required(ErrorMessage = "El codigo es obligatorio.")]
		[MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}.")]
		public string? Codigo { get; set; }


		// Nombre Plan  -------------------------------------------------------------------------------------------


		[Required(ErrorMessage = "El nombre del plan es obligatorio.")]
		[MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
		public string NombrePlan { get; set; }


		// Descripcion --------------------------------------------------------------------------------------------


		[MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
		public string? Descripcion { get; set; }


		// Cantidad de cuotas -------------------------------------------------------------------------------------


		[Required(ErrorMessage = "La cantidad de cuotas es obligatoria.")]
		[Range(1, int.MaxValue, ErrorMessage = "La cantidad de cuotas debe ser un valor positivo")]
		public int CantCuotas { get; set; }


		// Valor total del plan -----------------------------------------------------------------------------------


		[Required(ErrorMessage = "El valor total del plan es obligatorio.")]
		[Column(TypeName = "decimal(10,2)")]
		public decimal ValorTotal { get; set; }


		[Required(ErrorMessage = "El codigo es obligatorio.")]
		[MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}.")]
		public string CodigoVehiculo { get; set; } 
	}
}
