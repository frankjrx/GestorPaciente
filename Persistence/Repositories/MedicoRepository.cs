using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Domain.Entities;
using GestorPacientes.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Infrastructure.Persistence.Repositories
{
    public class MedicoRepository : GenericRepository<Medico>, IMedicoRepository
    {
        public MedicoRepository(DataBaseContext db) : base(db)
        {
        }
    }
}
