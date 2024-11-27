using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria.DB.Data.Entidades
{
    [Index(nameof(Codigo), Name = "TDocumento_UQ", IsUnique = true)]
    public class TipoDocumento : EntityBase
    {
        // Nombre ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El nombre del tipo de documento es obligatorio.")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }


        // Lista ------------------------------------------------------------------------------------------


        public List<Persona> Personas { get; set; } = new List<Persona>(); 
    }
}
