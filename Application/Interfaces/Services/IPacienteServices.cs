﻿using GestorPacientes.Core.Application.ViewModels.Pacientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.Interfaces.Services
{
    public interface IPacienteServices : IGenericSevice<SavePacienteVIewModel, PacienteViewModel>
    {
    }
}
