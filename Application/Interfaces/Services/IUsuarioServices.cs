using GestorPacientes.Core.Application.ViewModels.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.Interfaces.Services
{
    public interface IUsuarioServices : IGenericSevice<UsuarioSaveViewModel, UsuarioViewModel>
    {
        Task<UsuarioViewModel> Login(UserLoginViewModel vm);
        Task<UserEditVIewModel> GetUserEditById(int id);
    }
}
