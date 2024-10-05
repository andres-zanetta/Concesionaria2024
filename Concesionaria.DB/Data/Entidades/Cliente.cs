using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria.DB.Data.Entidades
{
    [Index(nameof(VehiculoId), Name = "PersonaId_UQ", IsUnique = true)]
    public class Cliente : EntityBase
    {
        // Fecha de inicio ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        public DateTime FechaInicio { get; set; }


        // Fecha fin ------------------------------------------------------------------------------------------


        public DateTime? FechaFin { get; set; }


        // Clase de navegación ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La persona es obligatoria.")]
        public int VehiculoId { get; set; }
        public Persona Persona { get; set; }


        // Lista ------------------------------------------------------------------------------------------


        public List<PlanVendido> planVendidos { get; set; } = new List<PlanVendido>();
    }
}
