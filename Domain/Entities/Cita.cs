using GestorPacientes.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Domain.Entities
{
   public class Cita : Auditable
    {
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }
        public int MedicoId { get; set; }
        public Medico? Medico { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string Causa { get; set; }
        public string? Estado { get; set; }

        public ICollection<Resultado>? resultados { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
