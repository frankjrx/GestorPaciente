using GestorPacientes.Core.Application.Helpers;
using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Medicos;
using GestorPacientes.Core.Application.ViewModels.Usuario;
using GestorPacientes.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestorPacientes.Core.Application.Services
{
    public class MedicoServices : IMedicoServices
    {
        private readonly IMedicoRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UsuarioViewModel userViewModel;

        public MedicoServices(IMedicoRepository repository, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            userViewModel = _contextAccessor.HttpContext.Session.Get<UsuarioViewModel>("User");
        }

        public async Task<SaveMedicoViewModel> Add(SaveMedicoViewModel vm)
        {
            Medico medico = new Medico() 
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Correo = vm.Correo,
                Cedula = vm.Cedula,
                Phone = vm.Phone,
                imgUrl = vm.imgUrl,
                UserId = userViewModel.Id
            };

            await _repository.CreateAsync(medico);

            SaveMedicoViewModel medicoViewModel = new SaveMedicoViewModel() 
            {
                Id = medico.Id,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Correo = medico.Correo,
                Cedula = medico.Cedula,
                Phone = medico.Phone,
                imgUrl = medico.imgUrl
            };
            return medicoViewModel;
        }

        public async Task Delete(int id)
        {
            var medico = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(medico);
        }

        public async Task<List<MedicoViewModel>> GetAll()
        {
            var medicoList = await _repository.GetAllAsync();
            return medicoList.Select(medico => new MedicoViewModel() 
            {
                Id = medico.Id,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Correo = medico.Correo,
                Cedula = medico.Cedula,
                Phone = medico.Phone,
                imgUrl = medico.imgUrl
            }).ToList();
        }

        public async Task<SaveMedicoViewModel> GetById(int id)
        {
            var medico = await _repository.GetByIdAsync(id);
            SaveMedicoViewModel vm = new SaveMedicoViewModel() 
            {
                Id = medico.Id,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Correo = medico.Correo,
                Cedula = medico.Cedula,
                Phone = medico.Phone,
                imgUrl = medico.imgUrl
            };

            return vm;
        }

        public async Task Update(SaveMedicoViewModel vm)
        {
            Medico medico = await _repository.GetByIdAsync (vm.Id);

            medico.Id = vm.Id;
            medico.Nombre = vm.Nombre;
            medico.Apellido = vm.Apellido;
            medico.Correo = vm.Correo;
            medico.Cedula = vm.Cedula;
            medico.Phone = vm.Phone;
            medico.imgUrl = vm.imgUrl;


            await _repository.UpdateAsync(medico);
        }
    }
}
