using GestorPacientes.Core.Application.ViewModels.Resultado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.Interfaces.Services
{
    public interface IResultadoServices : IGenericSevice<SaveResultadoViewModel, ResultadoViewModel>
    {
        Task<List<ResultadoViewModel>> GetAllWithInclude();
    }
}
