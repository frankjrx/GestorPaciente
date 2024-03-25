using GestorPacientes.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Domain.Entities
{
    public class Prueb : Auditable
    {
        public string Nombre { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Resultado>? resultados { get; set; }  
    }
}
