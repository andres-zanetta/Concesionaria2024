﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.GinoDTO.PlanVendido
{
    public class PUT_PlanVendidoDNI_DTO
    {

        public DateTime? FechaSuscripcion { get; set; }



        public DateTime? FechaSorteo { get; set; }


        // Descripción ----------------------------------------------------------------------------

        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Descripcion { get; set; }


        // Estado ---------------------------------------------------------------------------------


        [Required(ErrorMessage = "El estado del plan es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }


        // Fecha fin ------------------------------------------------------------------------------------------


        //Agregar metodo estatico para que la fecha fin no sea anterior a la fecha inicio
        public DateTime? FechaFin { get; set; }


        // Plan en mora ------------------------------------------------------------------------------------------


        [Display(Name = "¿El plan tiene mora?", Description = "Indica si el plan tiene mora")]
        public bool PlanEnMora { get; set; }

        //NavClass---------------------------------------------------------------------------------


        [Required(ErrorMessage = "El venderdor es obligatoria.")]
        public string VendedorDNI { get; set; }


        [Required(ErrorMessage = "El clienteo es obligatoria.")]
        public string ClienteDNI { get; set; }


        [Required(ErrorMessage = "El tipo de plan es obligatoria.")]
        public string TipoPlanCodigo { get; set; }

        public string? AdjudicaciónCodigo { get; set; }

    }
}
