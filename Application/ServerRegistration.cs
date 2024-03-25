using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Core.Application
{
    public static class ServerRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddTransient<IUsuarioServices, UsuarioServices>();
            services.AddTransient<IMedicoServices, MedicoServices>();
            services.AddTransient<IPacienteServices, PacienteServices>();
            services.AddTransient<IPruebaServices, PruebaServices>();
            services.AddTransient<IResultadoServices, ResultadoServices>();
            services.AddTransient<ICitaServices, CitaServices>();
        }
    }
}
