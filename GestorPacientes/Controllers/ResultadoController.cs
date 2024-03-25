using Microsoft.AspNetCore.Mvc;

using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Resultado;
using GestorPacientes.MiddleWare;

namespace GestorPacientes.Controllers
{
    public class ResultadoController : Controller
    {
        private readonly IResultadoServices _services;
		private readonly ValidateUserSession _validateUser;

		public ResultadoController(IResultadoServices services, IMedicoServices medicoServices, IPacienteServices pacienteServices, ValidateUserSession validateUser)
        {
            _services = services;
			_validateUser = validateUser;
		}

        public async Task<IActionResult> Index(string SearchString)
        {
            if (!_validateUser.HasUser()) 
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var ResultadoList = await _services.GetAllWithInclude();
            ResultadoList = ResultadoList.Where(r => r.estado == "Pendiente").ToList();
            if (!string.IsNullOrEmpty(SearchString)) 
            {
                ResultadoList = ResultadoList.Where(r => r.PacienteCedula.Contains(SearchString)).ToList();
            }
            return View(ResultadoList);
        }

        //----------------------------------Reportar-------------------------------------------------
        public async Task<IActionResult> Reportar(int id) 
        {
			if (!_validateUser.HasUser())
			{
				return RedirectToRoute(new { controller = "Home", action = "Index" });
			}
			var resultado = await _services.GetById(id);
            return View(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Reportar(SaveResultadoViewModel vm) 
        {
			if (!_validateUser.HasUser())
			{
				return RedirectToRoute(new { controller = "Home", action = "Index" });
			}
			vm.estado = "Completada";
            await _services.Update(vm);
            return RedirectToRoute(new {controller="Resultado", action="Index"});
        }


    }
}
