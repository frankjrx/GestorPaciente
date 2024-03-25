using GestorPacientes.Infrastructure.Persistence.Context;
using GestorPacientes.Core.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorPacientes.Infrastructure.Persistence.Repositories;

namespace GestorPacientes.Infrastructure.Persistence
{
    public static class ServerRegistration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration) 
        {
            if (configuration.GetValue<bool>("UseInMemoryDataBase"))
            {
                services.AddDbContext<DataBaseContext>(options => options.UseInMemoryDatabase("ApplicationDB"));
            }
            else 
            {
                services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                m => m.MigrationsAssembly(typeof(DataBaseContext).Assembly.FullName)));
            }


            //Inyecciones
            
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IMedicoRepository, MedicoRepository>();
            services.AddTransient<IPacienteRepository, PacienteRepository>();
            services.AddTransient<IPruebaRepository, PruebaRepository>();
            services.AddTransient<IResultadoRepository, ResultadoRepository>();
            services.AddTransient<ICitaRepository, CitaRepository>();
            
        }
    }
}
