using GestorPacientes.Core.Application.Helpers;
using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Resultado;
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
    public class ResultadoServices : IResultadoServices
    {
        private readonly IResultadoRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UsuarioViewModel userViewModel;

        public ResultadoServices(IResultadoRepository repository, IHttpContextAccessor contextAccessor) 
        {
            this._repository = repository;
            _contextAccessor = contextAccessor;
            userViewModel = _contextAccessor.HttpContext.Session.Get<UsuarioViewModel>("User");
        }
        public async Task<SaveResultadoViewModel> Add(SaveResultadoViewModel vm)
        {
            Resultado resultado = new Resultado() 
            {
                Id = vm.Id,
                PacienteId = vm.PacienteId,
                PruebaId = vm.PruebaId,
                CitaId = vm.CitaId,
                result = vm.result,
                estado = vm.estado,
                UserId = userViewModel.Id
            };

            await _repository.CreateAsync(resultado);

            var resultvm = new SaveResultadoViewModel() 
            {
                Id = resultado.Id,
                PacienteId = resultado.PacienteId,
                estado = resultado.estado,
				result = vm.result,
				PruebaId = resultado.PruebaId
            };

            return resultvm;
        }

        public async Task Delete(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(result);
        }

        public async Task<List<ResultadoViewModel>> GetAll()
        {
            var ResultList = await _repository.GetAllAsync();
            return ResultList.Select(r => new ResultadoViewModel() 
            {
                Id = r.Id,
                PacienteId = r.PacienteId,
                PruebaId = r.PruebaId,
                CitaId = r.CitaId,
				result = r.result,
				estado = r.estado
            }).ToList();
        }

        public async Task<List<ResultadoViewModel>> GetAllWithInclude()
        {
            var ResultList = await _repository.GetAllWithIncludeAsync(new List<string> { "Paciente", "Prueba"});
            return ResultList.Select(r => new ResultadoViewModel()
            {
                Id = r.Id,
                PacienteId = r.PacienteId,
                PruebaId = r.PruebaId,
                PacienteNombre = r.Paciente.Nombre,
                PacienteApellido = r.Paciente.Apellido,
                PacienteCedula = r.Paciente.Cedula,
                PruebaNombre = r.Prueba.Nombre,
                CitaId = r.CitaId,
				result = r.result,
				estado = r.estado

            }).ToList();
        }

        public async Task<SaveResultadoViewModel> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            SaveResultadoViewModel resultvm = new SaveResultadoViewModel() 
            {
                Id = result.Id,
                PacienteId = result.PacienteId,
                PruebaId = result.PruebaId,
				result = result.result,
				estado = result.estado
            };
            return resultvm;
        }

        public async Task Update(SaveResultadoViewModel vm)
        {
            Resultado resultado = await _repository.GetByIdAsync(vm.Id);
            resultado.Id = vm.Id;
            resultado.PacienteId = vm.PacienteId;
            resultado.PruebaId = vm.PruebaId;
            resultado.estado = vm.estado;
            resultado.result = vm.result;
            await _repository.UpdateAsync(resultado);
        }

    }
}
