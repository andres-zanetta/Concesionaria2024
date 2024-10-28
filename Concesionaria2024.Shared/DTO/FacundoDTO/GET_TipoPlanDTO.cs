using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.FacundoDTO
{
    public class GET_TipoPlanDTO
    {
        public int Id { get; set; }
        public string? NombrePlan { get; set; }
        public string? Descripcion { get; set; }
        public int CantCuotas { get; set; }
        public decimal ValorTotal { get; set; }
        public int VehiculoId { get; set; }
    }

}
