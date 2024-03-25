using GestorPacientes.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.ViewModels.Resultado
{
    public class ResultadoViewModel
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string PacienteNombre { get; set; }
        public string PacienteApellido { get; set; }
        public string PacienteCedula { get; set; }
        public int PruebaId { get; set; }
        public string PruebaNombre { get; set; }
        public string? result { get; set; }
		public string? estado { get; set; }

		public int CitaId { get; set; }
    }
}
