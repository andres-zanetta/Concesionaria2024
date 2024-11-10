using Concesionaria.DB.Data;
using Concesionaria.DB.Data.Entidades;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.GinoDTO.PlanVendido
{
    public class GET_PlanVendidoDTO : EntityBase
    {
        // Id -----------------------------------------------------------------------------------

        public int Id { get; set; }

        // Fecha cuando se suscribió ------------------------------------------------------------------------------------------


        public DateTime? FechaSuscripcion { get; set; }


        // FEcha del sorteo ------------------------------------------------------------------------------------------


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



        //  Clases de Nav pero en este caso las uso para traer info relac al plan




        [Required(ErrorMessage = "El cliente es obligatorio.")]
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; }



        [Required(ErrorMessage = "El vendedor es obligatorio.")]
        public int VendedorId { get; set; }
        public string NombreVendedor { get; set; }



        [Required(ErrorMessage = "El tipo de plan es obligatorio.")]
        public int TipoPlanId { get; set; }
        public string NombrePlan { get; set; }
        public decimal ValorTotal { get; set; }



        public int? AdjudicacionId { get; set; }
        public bool AutoEntregado { get; set; }
        public string PatenteVehiculo { get; set; }
        public string ModeloVehiculo { get; set; }
        public string MarcaVehiculo { get; set; }

    }
}
