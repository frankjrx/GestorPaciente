using GestorPacientes.Core.Application.Helpers;
using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Pacientes;
using GestorPacientes.Core.Application.ViewModels.Usuario;
using GestorPacientes.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestorPacientes.Core.Application.Services
{
    public class PacienteServices : IPacienteServices
    {
        private readonly IPacienteRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UsuarioViewModel userViewModel;

        public PacienteServices(IPacienteRepository repository, IHttpContextAccessor contextAccessor) 
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            userViewModel = _contextAccessor.HttpContext.Session.Get<UsuarioViewModel>("User");
        }
        public async Task<SavePacienteVIewModel> Add(SavePacienteVIewModel vm)
        {
            Paciente paciente = new Paciente() 
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Telefono = vm.Telefono,
                Direccion = vm.Direccion,
                Cedula = vm.Cedula,
                fnacimiento = vm.fnacimiento,
                Fumador = vm.Fumador,
                Alergias = vm.Alergias,
                imgUrl = vm.imgUrl,
                UserId = userViewModel.Id
            };

            await _repository.CreateAsync(paciente);

            SavePacienteVIewModel pacientevm = new SavePacienteVIewModel()
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                Telefono = paciente.Telefono,
                Direccion = paciente.Direccion,
                Cedula = paciente.Cedula,
                fnacimiento = paciente.fnacimiento,
                Fumador = paciente.Fumador,
                Alergias = paciente.Alergias,
                imgUrl = paciente.imgUrl
            };
            return pacientevm;
        }

        public async Task Delete(int id)
        {
            var paciente = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(paciente);
        }

        public async Task<List<PacienteViewModel>> GetAll()
        {
            var PacienteList = await _repository.GetAllAsync();
            return PacienteList.Select(p=>new PacienteViewModel() 
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                Telefono = p.Telefono,
                Direccion = p.Direccion,
                Cedula = p.Cedula,
                fnacimiento = p.fnacimiento,
                Fumador = p.Fumador,
                Alergias = p.Alergias,
                imgUrl = p.imgUrl
            }).ToList();
        }

        public async Task<SavePacienteVIewModel> GetById(int id)
        {
            var paciente = await _repository.GetByIdAsync(id);
            SavePacienteVIewModel pacientevm = new SavePacienteVIewModel() 
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                Telefono = paciente.Telefono,
                Direccion = paciente.Direccion,
                Cedula = paciente.Cedula,
                fnacimiento = paciente.fnacimiento,
                Fumador = paciente.Fumador,
                Alergias = paciente.Alergias,
                imgUrl = paciente.imgUrl
            };
            return pacientevm;
        }

        public async Task Update(SavePacienteVIewModel vm)
        {
            Paciente paciente = await _repository.GetByIdAsync (vm.Id);
            paciente.Id = vm.Id;
            paciente.Nombre = vm.Nombre;
            paciente.Apellido = vm.Apellido;
            paciente.Telefono = vm.Telefono;
            paciente.Direccion = vm.Direccion;
            paciente.Cedula = vm.Cedula;
            paciente.fnacimiento = vm.fnacimiento;
            paciente.Fumador = vm.Fumador;
            paciente.Alergias = vm.Alergias;
            paciente.imgUrl = vm.imgUrl;

            await _repository.UpdateAsync(paciente);
        }
    }
}
