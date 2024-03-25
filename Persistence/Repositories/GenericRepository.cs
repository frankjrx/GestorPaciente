using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly DataBaseContext db;

        public GenericRepository(DataBaseContext db) 
        {
            this.db = db;
        }
        public virtual async Task<Entity> CreateAsync(Entity entity)
        {
            await db.Set<Entity>().AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            db.Set<Entity>().Remove(entity);
            await db.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAllAsync()
        {
            return await db.Set<Entity>().ToListAsync();
        }

        public virtual async Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties)
        {
            var query = db.Set<Entity>().AsQueryable();

            foreach (string property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<Entity> GetByIdAsync(int id)
        {
            return await db.Set<Entity>().FindAsync(id);
        }

        public virtual async Task UpdateAsync(Entity entity)
        {
            db.Set<Entity>().Update(entity);
            await db.SaveChangesAsync();
        }
    }
}
