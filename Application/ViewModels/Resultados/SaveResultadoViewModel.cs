using GestorPacientes.Core.Application.ViewModels.Pacientes;
using GestorPacientes.Core.Application.ViewModels.Prueba;
using GestorPacientes.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.ViewModels.Resultado
{
    public class SaveResultadoViewModel
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe colocar un paciente")]
        public int PacienteId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe colocar una prueba")]
        public int PruebaId { get; set; }
        public List<PacienteViewModel>? pacientes { get; set; }
        public List<PruebaViewModel>? pruebas { get; set; }

        public string? result { get; set; }
		public string? estado { get; set; }

		public int[] PrueabaListId { get; set; }
        public int CitaId { get; set; }
    }
}
