﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.ViewModels.Usuario
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage ="Debe ingresar el nombre de usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set;}


        [Required(ErrorMessage = "Debe ingresar la contrasena")]
        [DataType(DataType.Password)]
        public string Password { get; set;}
    }
}
