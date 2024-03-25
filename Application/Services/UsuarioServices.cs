using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Usuario;
using GestorPacientes.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.Services
{
    public class UsuarioServices : IUsuarioServices
    {

        private readonly IUsuarioRepository _repository;

        public UsuarioServices(IUsuarioRepository repository) 
        {
            _repository = repository;
        }

        public async Task<UsuarioSaveViewModel> Add(UsuarioSaveViewModel vm)
        {
            User usuario = new User() 
            {
                Id = vm.Id,
                UserName = vm.UserName,
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Correo = vm.Correo,
                Password = vm.Password,
                TipoUsuario = vm.TipoUsuario,
                Estado = vm.Estado
            };
            await _repository.CreateAsync(usuario);
            UsuarioSaveViewModel uservm = new UsuarioSaveViewModel()
            {
                Id = usuario.Id,
                UserName = usuario.UserName,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                Password = usuario.Password,
                TipoUsuario = usuario.TipoUsuario,
                Estado = usuario.Estado
            };

            return uservm;
        }

        public async Task Delete(int id)
        {
            User user = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(user);
        }

        public async Task<List<UsuarioViewModel>> GetAll()
        {
            var userList = await _repository.GetAllAsync();
            return userList.Select(u => new UsuarioViewModel()
            {
                Id = u.Id,
                UserName = u.UserName,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Correo= u.Correo,
                TipoUsuario= u.TipoUsuario,
                Estado = u.Estado
            }).ToList();
        }

        public async Task<UsuarioSaveViewModel> GetById(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            UsuarioSaveViewModel uservm = new UsuarioSaveViewModel() 
            {
                Id = user.Id,
                UserName = user.UserName,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Correo= user.Correo,
                Password = user.Password,
                TipoUsuario = user.TipoUsuario,
                Estado = user.Estado
            };

            return uservm;
        }

        public async Task Update(UsuarioSaveViewModel vm)
        {
            var existingUser = await _repository.GetByIdAsync(vm.Id);

            if (existingUser != null)
            {
                existingUser.UserName = vm.UserName;
                existingUser.Nombre = vm.Nombre;
                existingUser.Apellido = vm.Apellido;
                existingUser.Correo = vm.Correo;
                existingUser.Password = vm.Password;
                existingUser.TipoUsuario = vm.TipoUsuario;
                existingUser.Estado = vm.Estado;

                await _repository.UpdateAsync(existingUser);
            }
        }

        public async Task<UsuarioViewModel> Login(UserLoginViewModel vm)
        {
            User user = await _repository.LoginAsync(vm);
            if (user == null) 
            {
                return null;
            }

            UsuarioViewModel uservm = new UsuarioViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Correo = user.Correo,
                TipoUsuario = user.TipoUsuario
            };

            return uservm;
        }

        public async Task<UserEditVIewModel> GetUserEditById(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            UserEditVIewModel uservm = new UserEditVIewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Correo = user.Correo,
                Estado = user.Estado,
                Password = user.Password
            };

            return uservm;
        }
    }
}
