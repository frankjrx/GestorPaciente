using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.Interfaces.Services
{
    public interface IGenericSevice<SaveViewModel, ViewModel>
        where SaveViewModel : class
        where ViewModel : class
    {
        Task<SaveViewModel> Add(SaveViewModel vm);
        Task Update(SaveViewModel vm);
        Task Delete(int id);
        Task<SaveViewModel> GetById(int id);
        Task<List<ViewModel>> GetAll();
    }
}
