using GestorPacientes.Core.Application.ViewModels.Usuario;
using GestorPacientes.Core.Application.Helpers;


namespace GestorPacientes.MiddleWare
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ValidateUserSession(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool HasUser() 
        {
            UsuarioViewModel user = _contextAccessor.HttpContext.Session.Get<UsuarioViewModel>("User");
            if (user == null) 
            {
                return false;
            }

            return true;
        }
    }
}
