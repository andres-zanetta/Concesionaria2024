using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Concesionaria2024.Shared.DTO.FacundoDTO
{
    namespace Concesionaria2024.Shared.DTO.FacundoDTO
    {
        public class GET_VehiculoDTO
        {
			// Codigo -----------------------------------------------------------------------------------------

			[Required(ErrorMessage = "El codigo del Vehiculo es obligatoria.")]
			[MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}.")]
			public string Codigo { get; set; }

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
}
