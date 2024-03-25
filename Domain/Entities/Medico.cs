using GestorPacientes.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Domain.Entities
{
    public class Medico : Auditable
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Phone { get; set; }
        public string Cedula { get; set;}
        public string? imgUrl { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Cita> Citas { get; set; }
    }
}
