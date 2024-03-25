using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.ViewModels.Prueba
{
    public class SavePruebaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombew")]
        public string Nombre { get; set; }
    }
}
