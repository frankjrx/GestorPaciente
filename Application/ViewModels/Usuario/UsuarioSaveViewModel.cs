using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.ViewModels.Usuario
{
    public class UsuarioSaveViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Debe colocar su nombre")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "Debe colocar su Apellido")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }


        [Required(ErrorMessage = "Debe colocar su correo")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }


        [Required(ErrorMessage = "Debe colocar un tipo de usuario")]
        [DataType(DataType.Text)]
        public string TipoUsuario { get; set; }


        [Required(ErrorMessage = "Debe colocar una contrasena")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Compare(nameof(Password), ErrorMessage ="Las contrasenas deben de ser iguales")]
        [Required(ErrorMessage = "Debe colocar una contrasena")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string? Estado { get; set; }

    }
}
