using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestorPacientes.Core.Application.Helpers;
using Microsoft.EntityFrameworkCore;
using GestorPacientes.MiddleWare;

namespace GestorPacientes.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsuarioServices _service;
        private readonly ValidateUserSession _validateUser;
        public UserController(IUsuarioServices service, ValidateUserSession validateUser) 
        {
            _service = service;
            _validateUser = validateUser;
        }
        
        public async Task<IActionResult> Index()
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var usuarios = await _service.GetAll();
            return View(usuarios);
        }

        //-------------------------------------------------------------Agregar Usuario----------------
        public IActionResult Register()
        {
           
            return View(new UsuarioSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UsuarioSaveViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            vm.Estado = "activo";
            await _service.Add(vm);
            return RedirectToRoute(new {controller = "User", action = "Index"});
        }

        //-----------------------------------------------------------------------Editar Usuario-------------------

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var user = await _service.GetUserEditById(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditVIewModel userEdit)
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View(userEdit);
            }


            var user = await _service.GetById(userEdit.Id);

            user.Nombre = userEdit.Nombre;
            user.Apellido = userEdit.Apellido;
            user.UserName = userEdit.UserName;
            user.Correo = userEdit.Correo;
            //user.TipoUsuario = user.TipoUsuario;
            //user.Estado = user.Estado;
            user.Password = userEdit.Password;

            await _service.Update(user);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        //-----------------------------------------------------Eliminar---------------------------------------------------------
        public async Task<IActionResult> Delete(int id) 
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(await _service.GetById(id));
        }


        public async Task<IActionResult> DeletePost(int id) 
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            var user = await _service.GetById(id);

            user.Estado = "inactivo";
         

            await _service.Update(user);
            return RedirectToRoute(new { controller = "User", action = "Index" });

        }
    }
}
