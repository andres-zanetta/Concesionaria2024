using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria.DB.Data
{
    public class EntityBase : IEntityBase
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "El codigo es obligatorio.")]
		[MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}.")]
		public string? Codigo { get; set; }

    }
}
