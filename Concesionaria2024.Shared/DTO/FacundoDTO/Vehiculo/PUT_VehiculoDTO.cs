using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concesionaria2024.Shared.DTO.FacundoDTO.Vehiculo
{
    public class PUT_VehiculoDTO
    {
		// Marca ------------------------------------------------------------------------------------------


		[Required(ErrorMessage = "La marca del Vehiculo es obligatoria.")]
		[MaxLength(15, ErrorMessage = "Máximo número de caracteres {1}.")]
		public string Marca { get; set; }


		// Modelo -----------------------------------------------------------------------------------------


		[Required(ErrorMessage = "El modelo del Vehiculo es obligatorio.")]
		[MaxLength(15, ErrorMessage = "Máximo número de caracteres {1}.")]
		public string Modelo { get; set; }


		// Precio -----------------------------------------------------------------------------------------


		[Required(ErrorMessage = "El precio del Vehiculo es obligatorio.")]
		[Column(TypeName = "decimal(10,2)")]
		public decimal Precio { get; set; }


		// Motor ------------------------------------------------------------------------------------------


		[MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
		public string? Motor { get; set; }


		// Combustible ------------------------------------------------------------------------------------


		[MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
		public string? Combustible { get; set; }
	}
}