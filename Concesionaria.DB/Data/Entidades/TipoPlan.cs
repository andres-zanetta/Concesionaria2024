using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.DB.Data.Entidades
{
    [Index(nameof(NombrePlan), Name = "NombrePlan_UQ", IsUnique = true)]
    public class TipoPlan : EntityBase
    {
        // Nombre ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El nombre del plan es obligatorio.")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string NombrePlan { get; set; }


        // Descripcion ------------------------------------------------------------------------------------------


        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Descripcion { get; set; }


        // Cantidad de cuotas -------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La cantidad de cuotas es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de cuotas debe ser un valor positivo")]
        public int CantCuotas { get; set; }


        // Valor total del plan ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El valor total del plan es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorTotal { get; set; }


        // Clase de navegación ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El Vehiculo es obligatorio.")]
        public int VehiculoId { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }
}
