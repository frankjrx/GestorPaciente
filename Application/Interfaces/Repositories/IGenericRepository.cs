using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity> CreateAsync(Entity entity);
        Task UpdateAsync(Entity entity);
        Task DeleteAsync(Entity entity);
        Task<Entity> GetByIdAsync(int id);
        Task<List<Entity>> GetAllAsync();
        Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties);
    }
}
