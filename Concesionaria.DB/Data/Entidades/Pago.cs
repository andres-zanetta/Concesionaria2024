using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria.DB.Data.Entidades
{
    [Index(nameof(CuotaId), Name = "CuotaId_UQ", IsUnique = true)]
    public class Pago : EntityBase
    {

        // Fecha pago ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La fecha del pago es obligatoria.")]
        public DateTime FechaPago { get; set; }


        // Metodo de pago ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El metodo de pago es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string MetodoPago { get; set; }


        // REferencia del pago ------------------------------------------------------------------------------------------

        
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? ReferenciaPago { get; set; }


        // Clase de navegación ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La cuota asociada es obligatoria.")]
        public int CuotaId { get; set; }
        public Cuota Cuota { get; set; }
    }
}
