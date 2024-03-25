using GestorPacientes.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestorPacientes.Core.Domain.Entities
{
    public class Paciente : Auditable
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion {  get; set; }
        public string Cedula { get; set; }
        public DateOnly fnacimiento { get; set; }
        public bool Fumador { get; set; }
        public string Alergias { get; set; }
        public string? imgUrl { get; set; }

        
        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Cita>? Citas { get; set; }
        public ICollection<Resultado>? resultados { get; set; }
    }
}
