using GestorPacientes.Core.Application.Helpers;
using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Prueba;
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
    public class PruebaServices : IPruebaServices
    {
        private readonly IPruebaRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UsuarioViewModel userViewModel;

        public PruebaServices(IPruebaRepository repository, IHttpContextAccessor contextAccessor) 
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            userViewModel = _contextAccessor.HttpContext.Session.Get<UsuarioViewModel>("User");
        }
        public async Task<SavePruebaViewModel> Add(SavePruebaViewModel vm)
        {
            Prueb prueba = new Prueb() 
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                UserId = userViewModel.Id
            };

            await _repository.CreateAsync(prueba);

            SavePruebaViewModel viewModel = new SavePruebaViewModel() 
            {
                Id = prueba.Id,
                Nombre = prueba.Nombre
            };

            return viewModel;
        }

        public async Task Delete(int id)
        {
            var prueba = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(prueba);  
        }

        public async Task<List<PruebaViewModel>> GetAll()
        {
            var PruebaList = await _repository.GetAllAsync();
            return PruebaList.Select(p => new PruebaViewModel() 
            {
                Id = p.Id,
                Nombre = p.Nombre
            }).ToList();
        }

        public async Task<SavePruebaViewModel> GetById(int id)
        {
            var prueba = await _repository.GetByIdAsync(id);
            SavePruebaViewModel pruebavm = new SavePruebaViewModel() 
            {
                Id = prueba.Id,
                Nombre = prueba.Nombre
            };
            return pruebavm;
        }

        public async Task Update(SavePruebaViewModel vm)
        {


            Prueb prueba = await _repository.GetByIdAsync(vm.Id);
            prueba.Id = vm.Id;
            prueba.Nombre = vm.Nombre;

            await _repository.UpdateAsync(prueba);
        }
    }
}
