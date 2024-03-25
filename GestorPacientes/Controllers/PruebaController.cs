using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Prueba;
using GestorPacientes.MiddleWare;
using Microsoft.AspNetCore.Mvc;

namespace GestorPacientes.Controllers
{
    public class PruebaController : Controller
    {
        private readonly IPruebaServices _services;
        private readonly ValidateUserSession _validateUser;
        public PruebaController(IPruebaServices services, ValidateUserSession validateUser) 
        {
            _services = services;
            _validateUser = validateUser;
        }
        public async Task<IActionResult> Index()
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(await _services.GetAll());
        }

        //---------------------------------------------------------Create-------------------------------------------
        public IActionResult Create() 
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View("SavePrueba", new SavePruebaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePruebaViewModel vm) 
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid) 
            {
                return View("SavePrueba", vm);
            }

            await _services.Add(vm);
			return RedirectToRoute(new { controller = "Prueba", action = "Index" });
		}
        //---------------------------------------------------------Edit------------------------------------------
        public async Task<IActionResult> Edit(int id) 
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View("SavePrueba", await _services.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePruebaViewModel vm) 
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid) 
            {
                return View("SavePrueba", vm);
            }

            await _services.Update(vm);
            return RedirectToRoute(new {controller="Prueba", action="Index" });
        }

        //--------------------------------------------------------Delete--------------------------------------------------
        public async Task<IActionResult> Delete(int id) 
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(await _services.GetById(id));
        }

        public async Task<IActionResult> DeletePost(int id) 
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            await _services.Delete(id);
            return RedirectToRoute(new {action="Index", controller="Prueba"});
        }
    }
}
