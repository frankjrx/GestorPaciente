using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.ViewModels.Medicos
{
    public class SaveMedicoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe colocar un Apellido")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Debe colocar un Correo")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Debe colocar un telefono")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Debe colocar la cedula")]
        [DataType(DataType.Text)]
        public string Cedula { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? Foto { get; set; }

        public string? imgUrl { get; set; }
    }
}
