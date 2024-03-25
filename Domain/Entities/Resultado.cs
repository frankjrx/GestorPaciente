using GestorPacientes.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Domain.Entities
{
    public class Resultado : Auditable
    {
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }
        public int PruebaId { get; set; }
        public Prueb? Prueba { get; set; }
        public int CitaId { get; set; }
        public Cita? Cita { get; set; }
        public string? result { get; set; }
		public string? estado { get; set; }

		public int UserId { get; set; }
        public User? User { get; set; }

    }
}
