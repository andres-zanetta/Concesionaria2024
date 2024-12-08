﻿using Concesionaria.DB.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionaria2024.Shared.DTO.AndresDTO
{
    public class POST_VendedorDTO
    {


		[Required(ErrorMessage = "El número de documento es obligatorio.")]
		[MaxLength(15, ErrorMessage = "Máximo número de caracteres {1}.")]
		public string NumDoc { get; set; }


	}
}
