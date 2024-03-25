using GestorPacientes.Core.Application.ViewModels.Medicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.Interfaces.Services
{
    public interface IMedicoServices : IGenericSevice<SaveMedicoViewModel, MedicoViewModel>
    {
    }
}
