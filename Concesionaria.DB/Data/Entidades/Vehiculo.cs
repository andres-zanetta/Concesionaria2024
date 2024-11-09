using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria.DB.Data.Entidades
{
    [Index(nameof(Codigo), Name = "Codigo_UQ", IsUnique = true)]
    [Index(nameof(Marca), nameof(Modelo), Name = "MArca_Modelo_UQ", IsUnique = true)]
    public class Vehiculo : EntityBase
    {
        // Codigo -----------------------------------------------------------------------------------------

        [Required(ErrorMessage = "El codigo del Vehiculo es obligatoria.")]
        [MaxLength(30, ErrorMessage = "Máximo número de caracteres {1}.")]
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


        // Lista ------------------------------------------------------------------------------------------


        public List<TipoPlan> TipoPlanes { get; set; } = new List<TipoPlan>();
        public List<Adjudicacion> Adjudicaciones { get; set; } = new List<Adjudicacion>();

    }
}
