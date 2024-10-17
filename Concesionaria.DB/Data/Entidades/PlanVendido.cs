using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria.DB.Data.Entidades
{
    [Index(nameof(ClienteId), Name = "ClienteId_UQ", IsUnique = false)]
    [Index(nameof(VendedorId), Name = "VendedorId_UQ", IsUnique = false)]
    [Index(nameof(Estado), Name = "Estado", IsUnique = false)]
    public class PlanVendido : EntityBase
    {
        // Fecha cuando se suscribió ------------------------------------------------------------------------------------------


        public DateTime? FechaSuscripcion { get; set; }


        // Fecha del sorteo ------------------------------------------------------------------------------------------


        public DateTime? FechaSorteo { get; set; }


        // Descripción ------------------------------------------------------------------------------------------



        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Descripcion { get; set; }


        // Estado ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El estado del plan es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Estado { get; set; }


        // Fecha inicio ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }


        // Fecha fin ------------------------------------------------------------------------------------------


        //Agregar metodo estatico para que la fecha fin no sea anterior a la fecha inicio
        public DateTime? FechaFin { get; set; }


        // Plan en mora ------------------------------------------------------------------------------------------


        [Display(Name = "¿El plan tiene mora?", Description = "Indica si el plan tiene mora")]
        public bool PlanEnMora { get; set; }


        // Clase de navegación ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El cliente es obligatorio.")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }


        [Required(ErrorMessage = "El vendedor es obligatorio.")]
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }


        [Required(ErrorMessage = "El tipo de plan es obligatorio.")]
        public int TipoPlanId { get; set; }
        public TipoPlan TipoPlan { get; set; }


        public int? AdjudicacionId { get; set; }
        public Adjudicacion? Adjudicacion { get; set; }


        // Lista ------------------------------------------------------------------------------------------


        public List<Cuota> cuotas { get; set; } = new List<Cuota>();
    }
}
