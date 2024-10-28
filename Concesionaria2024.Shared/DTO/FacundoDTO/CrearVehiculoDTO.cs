﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concesionaria2024.Shared.DTO.FacundoDTO
{
    public class CrearVehiculoDTO
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public string Color { get; set; }
        public decimal Precio { get; set; }
    }
}
