﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria.DB.Data.Entidades
{
    [Index(nameof(Marca), nameof(Modelo), Name = "MArca_Modelo_UQ", IsUnique = true)]
    public class Vehiculo : EntityBase
    {
        // Marca ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La marca del Vehiculo es obligatoria.")]
        [MaxLength(15, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Marca { get; set; }


        // Modelo ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El modelo del Vehiculo es obligatorio.")]
        [MaxLength(15, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Modelo { get; set; }


        // Precio ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El precio del Vehiculo es obligatorio.")]
        [Range(1,int.MaxValue, ErrorMessage = "El precio del vehiculo debe ser un numero positivo")]
        public decimal Precio { get; set; }


        // Motor ------------------------------------------------------------------------------------------


        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Motor { get; set; }


        // Combustible ------------------------------------------------------------------------------------------


        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Combustible { get; set; }


        // Lista ------------------------------------------------------------------------------------------


        public List<TipoPlan> TipoPlanes { get; set; } = new List<TipoPlan>();
        public List<Adjudicacion> adjudicaciones { get; set; } = new List<Adjudicacion>();
    }
}