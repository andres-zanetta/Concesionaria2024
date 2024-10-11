using Concesionaria.DB.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.GinoDTO
{
    public class GET_PersonaDTO : EntityBase
    {
        // Nombre ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }


        // Apellido ------------------------------------------------------------------------------------------


        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Apellido { get; set; }
        //Nulo para que permita dejar en blanco si es empresa 


        // Direccion ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [MaxLength(200, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Direccion { get; set; }


        // Telefono ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "La dirección es obligatoria.")]
        // Agregar metodo estatico para validar que solo se introduzcan numero y no letras o caract especiales
        public string Telefono { get; set; }


        // Email ------------------------------------------------------------------------------------------


        [EmailAddress(ErrorMessage = "El formato introducido no es valido")]
        public string? Email { get; set; }


        // Es empresa ------------------------------------------------------------------------------------------


        [Display(Name = "¿Los datos de persona corresponden a una persona juridica?",
                 Description = "Indica si los datos cargados son de una empresa")]
        public bool EsEmpresa { get; set; }


        // Número documento ------------------------------------------------------------------------------------------


        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        //Agregar metodo que valide que sea un número del 1 al 15
        public int NumDoc { get; set; }


        // Tipo Documento ------------------------------------------------------------------------------------------
        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        public int TipoDocumentoId { get; set; }

        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        public string TipoDocumentoNombre { get; set; }
    }
}
