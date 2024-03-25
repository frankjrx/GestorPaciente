using GestorPacientes.Core.Application.Helpers;
using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Citas;
using GestorPacientes.Core.Application.ViewModels.Usuario;
using GestorPacientes.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.Services
{
    public class CitaServices : ICitaServices
    {

        private readonly ICitaRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UsuarioViewModel userViewModel;

        public CitaServices(ICitaRepository repository, IHttpContextAccessor contextAccessor) 
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            userViewModel = _contextAccessor.HttpContext.Session.Get<UsuarioViewModel>("User");
        }
        public async Task<SaveCitaViewModel> Add(SaveCitaViewModel vm)
        {
            Cita cita = new Cita() 
            {
                Id = vm.Id,
                PacienteId = vm.PacienteId,
                MedicoId = vm.MedicoId,
                Fecha = vm.Fecha,
                Hora = vm.Hora,
                Causa = vm.Causa,
                Estado = vm.Estado,
                UserId = userViewModel.Id
            };

            await _repository.CreateAsync(cita);

            SaveCitaViewModel citavm = new SaveCitaViewModel() 
            {
                Id = cita.Id,
                PacienteId = cita.PacienteId,
                MedicoId = cita.MedicoId,
                Fecha = cita.Fecha,
                Hora = cita.Hora,
                Causa = cita.Causa,
                Estado = cita.Estado
            };

            return citavm;

        }

        public async Task Delete(int id)
        {
            var cita = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(cita);
        }

        public async Task<List<CitaViewModel>> GetAll()
        {
            var CitaList = await _repository.GetAllAsync();
            return CitaList.Select(cita=>new CitaViewModel() 
            {
                Id = cita.Id,
                PacienteId = cita.PacienteId,
                MedicoId = cita.MedicoId,
                Fecha = cita.Fecha,
                Hora = cita.Hora,
                Causa = cita.Causa,
                Estado = cita.Estado
            }).ToList();
        }

        public async Task<List<CitaViewModel>> GetAllWithInclude()
        {
			var CitaList = await _repository.GetAllWithIncludeAsync(new List<string> { "Paciente", "Medico" });
			return CitaList.Select(cita => new CitaViewModel()
			{
				Id = cita.Id,
				PacienteId = cita.PacienteId,
				MedicoId = cita.MedicoId,
                PacienteNombre = cita.Paciente.Nombre,
                MedicoNombre = cita.Medico.Nombre,
				Fecha = cita.Fecha,
				Hora = cita.Hora,
				Causa = cita.Causa,
				Estado = cita.Estado
			}).ToList();
		}

        public async Task<SaveCitaViewModel> GetById(int id)
        {
            var cita = await _repository.GetByIdAsync(id);
            SaveCitaViewModel citavm = new SaveCitaViewModel() 
            {
                Id = cita.Id,
                PacienteId = cita.PacienteId,
                MedicoId = cita.MedicoId,
                Fecha = cita.Fecha,
                Hora = cita.Hora,
                Causa = cita.Causa,
                Estado = cita.Estado
            };

            return citavm;
        }

        public async Task Update(SaveCitaViewModel vm)
        {
            Cita cita = await _repository.GetByIdAsync(vm.Id);
            cita.Id = vm.Id;
            cita.PacienteId = vm.PacienteId;
            cita.MedicoId = vm.MedicoId;
            cita.Fecha = vm.Fecha;
            cita.Hora = vm.Hora;
            cita.Causa = vm.Causa;
            cita.Estado = vm.Estado;

            await _repository.UpdateAsync(cita);
        }

	}
}
