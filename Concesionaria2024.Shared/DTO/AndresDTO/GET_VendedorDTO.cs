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
    public class GET_VendedorDTO:EntityBase
    {
        [Required(ErrorMessage = "El vendedor es obligatorio.")]
        public int VendedorId { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un numero positivo")]
        public int? CantPlanesVendidos { get; set; }


        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public Persona NombrePersona { get; set; }



    }
}
