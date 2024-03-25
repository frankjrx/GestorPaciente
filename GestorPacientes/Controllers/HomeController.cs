using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestorPacientes.Core.Application.Helpers;
using GestorPacientes.MiddleWare;

namespace GestorPacientes.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUsuarioServices _service;
        private readonly ValidateUserSession _validateUser;

        public HomeController(IUsuarioServices service, ValidateUserSession validateUser)
        {
            _service = service;
            _validateUser = validateUser;
        }

        //--------------------------------------------------------------Login-----------------------------------------
        public IActionResult Index()
        {
            
            if (_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserLoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            UsuarioViewModel uservm = await _service.Login(vm);
            if (uservm != null)
            {
                HttpContext.Session.Set<UsuarioViewModel>("User", uservm);
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("UserValidation", "Datos de acceso incorrectos");
            }
            return View(vm);
        }


        //-------------------------------------------------------------Logout----------------
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("User");
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
