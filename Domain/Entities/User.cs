using GestorPacientes.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Domain.Entities
{
    public class User : Auditable
    {
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string TipoUsuario { get; set; }
        public string? Estado { get; set; }

        public ICollection<User>? Users { get; set; }
        public ICollection<Medico>? Medicos { get; set; }
        public ICollection<Paciente>? Pacientes { get; set; }
        public ICollection<Prueb>? Pruebas { get; set; }
        public ICollection<Cita>? Citas { get; set; }
        public ICollection<Resultado>? Resultados { get; set; }

    }
}
