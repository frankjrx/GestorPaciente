using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.ViewModels.Pacientes
{
    public class SavePacienteVIewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Debe colocar el nombre")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "Debe colocar el apellido")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }


        [Required(ErrorMessage = "Debe colocar el telefono")]
        [DataType(DataType.Text)]
        public string Telefono { get; set; }


        [Required(ErrorMessage = "Debe colocar la direccion")]
        [DataType(DataType.Text)]
        public string Direccion { get; set; }


        [Required(ErrorMessage = "Debe colocar la cedula")]
        [DataType(DataType.Text)]
        public string Cedula { get; set; }


        [Required(ErrorMessage = "Debe colocar la fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateOnly fnacimiento { get; set; }


        [Required(ErrorMessage = "Debe colocar si es fumador o no")]
        public bool Fumador { get; set; }


        [Required(ErrorMessage = "Debe colocar las alergias(o si no tiene)")]
        [DataType(DataType.Text)]
        public string Alergias { get; set; }


        [DataType(DataType.Upload)]
        public IFormFile? Foto { get; set; }

        public string? imgUrl { get; set; }
    }
}
