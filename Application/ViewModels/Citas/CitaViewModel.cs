using GestorPacientes.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.ViewModels.Citas
{
    public class CitaViewModel
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string PacienteNombre { get; set; }
        public int MedicoId { get; set; }
		public string MedicoNombre { get; set; }
		public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string Causa { get; set; }
        public string Estado { get; set; }
    }
}
