using GestorPacientes.Core.Application.Helpers;
using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Application.ViewModels.Usuario;
using GestorPacientes.Core.Domain.Entities;
using GestorPacientes.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : GenericRepository<User>, IUsuarioRepository
    {
        private readonly DataBaseContext db;
        public UsuarioRepository(DataBaseContext db) : base(db)
        {
            this.db = db;
        }

        public override async Task<User> CreateAsync(User entity)
        {
            entity.Password = PasswordEncryption.ComputeSha256Hash(entity.Password);
            return await base.CreateAsync(entity);
        }

        public async Task<User> LoginAsync(UserLoginViewModel entity)
        {
            string PasswordEncrypt = PasswordEncryption.ComputeSha256Hash(entity.Password);
            User user = await db.Set<User>().FirstOrDefaultAsync(user=>user.UserName == entity.UserName && user.Password == PasswordEncrypt);
            return user;

        }
    }
}
