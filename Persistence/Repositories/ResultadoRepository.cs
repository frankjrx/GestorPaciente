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
    public class ResultadoRepository : GenericRepository<Resultado>, IResultadoRepository
    {
        public ResultadoRepository(DataBaseContext db) : base(db)
        {
        }
    }
}
