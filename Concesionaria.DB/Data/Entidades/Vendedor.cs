using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria.DB.Data.Entidades
{
    [Index(nameof(PersonaId), Name = "PersonaId_UQ", IsUnique = true)]
    public class Vendedor : EntityBase
    {
        // Cantidad de planes vendidos ------------------------------------------------------------------------------------------


        [Range(1, int.MaxValue, ErrorMessage = "El precio del vehiculo debe ser un numero positivo")]
        public int? CantPlanesVendidos { get; set; }


        // Fecha inicio ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }


        // Fecha fin ------------------------------------------------------------------------------------------


        //Agregar metodo estatico para que la fecha fin no sea anterior a la fecha inicio
        public DateTime? FechaFin { get; set; }


        // Clase de navegación ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La persona es obligatoria.")]
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }


        // Lista ------------------------------------------------------------------------------------------


        public List<PlanVendido> planVendidos { get; set; } = new List<PlanVendido>();
    }
}
