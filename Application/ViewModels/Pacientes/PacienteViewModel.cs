using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.ViewModels.Pacientes
{
    public class PacienteViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Cedula { get; set; }
        public DateOnly fnacimiento { get; set; }
        public bool Fumador { get; set; }
        public string Alergias { get; set; }
        public string imgUrl { get; set; }
    }
}
