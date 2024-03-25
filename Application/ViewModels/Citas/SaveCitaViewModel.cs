using GestorPacientes.Core.Application.ViewModels.Medicos;
using GestorPacientes.Core.Application.ViewModels.Pacientes;
using GestorPacientes.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.ViewModels.Citas
{
    public class SaveCitaViewModel
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un paciente")]
        public int PacienteId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un medico")]
        public int MedicoId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la fecha")]
        [DataType(DataType.Date)]
        public DateOnly Fecha { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la fecha")]
        [DataType(DataType.Time)]
        public TimeOnly Hora { get; set; }


        [Required(ErrorMessage = "Debe colocar la casa")]
        [DataType(DataType.Text)]
        public string Causa { get; set; }

        public string? Estado { get; set; }


        public List<PacienteViewModel>? pacientes { get; set; }
        public List<MedicoViewModel>? medicos { get; set; }
    }
}
