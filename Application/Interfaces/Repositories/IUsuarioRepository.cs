using GestorPacientes.Core.Application.ViewModels.Usuario;
using GestorPacientes.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository : IGenericRepository<User>
    {
        Task<User> LoginAsync(UserLoginViewModel entity);
    }
}
