using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.Services;
using GestorPacientes.Core.Application.ViewModels.Citas;
using GestorPacientes.Core.Application.ViewModels.Resultado;
using GestorPacientes.Core.Domain.Entities;
using GestorPacientes.MiddleWare;
using Microsoft.AspNetCore.Mvc;

namespace GestorPacientes.Controllers
{
	public class CitaController : Controller
	{
		private readonly ICitaServices _services;
		private readonly IMedicoServices medicoServices;
		private readonly IPacienteServices pacienteServices;
		private readonly IResultadoServices resultadoServices;
		private readonly IPruebaServices pruebaServices;
		private readonly ValidateUserSession _validateUser;
		public CitaController(ICitaServices services, IMedicoServices medicoServices, IPacienteServices pacienteServices, IResultadoServices resultadoServices, IPruebaServices pruebaServices, ValidateUserSession validateUser) 
		{
			_services = services;
			this.medicoServices = medicoServices;
			this.pacienteServices = pacienteServices;
			this.resultadoServices = resultadoServices;
			this.pruebaServices = pruebaServices;
			_validateUser = validateUser;

		}
		public async Task<IActionResult> Index()
		{
			if (!_validateUser.HasUser())
			{
				return RedirectToRoute(new { controller = "Home", action = "Index" });
			}
			return View(await _services.GetAllWithInclude());
		}


		//-------------------------------Crear-----------------------------
		public async Task<IActionResult> Create() 
		{
			if (!_validateUser.HasUser())
			{
				return RedirectToRoute(new { controller = "Home", action = "Index" });
			}
			SaveCitaViewModel vm = new();
			vm.medicos = await medicoServices.GetAll();
			vm.pacientes = await pacienteServices.GetAll();
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> Create(SaveCitaViewModel vm) 
		{
			if (!_validateUser.HasUser())
			{
				return RedirectToRoute(new { controller = "Home", action = "Index" });
			}
			if (!ModelState.IsValid) 
			{
				vm.medicos = await medicoServices.GetAll();
				vm.pacientes = await pacienteServices.GetAll();
				return View(vm);
			}

			vm.Estado = "Pendiente de Consulta";
			await _services.Add(vm);
			return RedirectToRoute(new {controller="Cita", action="index"});
		}

		//-----------------------------------ELiminar-----------------------------------
		public async Task<IActionResult> Delete(int id) 
		{
			if (!_validateUser.HasUser())
			{
				return RedirectToRoute(new { controller = "Home", action = "Index" });
			}
			return View(await _services.GetById(id));
		}

		[HttpPost]
		public async Task<IActionResult> DeletePost(int id) 
		{
			if (!_validateUser.HasUser())
			{
				return RedirectToRoute(new { controller = "Home", action = "Index" });
			}
			await _services.Delete(id);

			return RedirectToRoute(new { controller = "Cita", action = "Index" });
		}

		//----------------------------------Consultar--------------------------------------
		public async Task<IActionResult> Consultar(SaveCitaViewModel cita) 
		{
			if (!_validateUser.HasUser())
			{
				return RedirectToRoute(new { controller = "Home", action = "Index" });
			}
			SaveResultadoViewModel vm = new();
			vm.PacienteId = cita.PacienteId;
			vm.pruebas = await pruebaServices.GetAll();
			vm.CitaId = cita.Id;
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> Consultar(SaveResultadoViewModel vm) 
		{
			if (!_validateUser.HasUser())
			{
				return RedirectToRoute(new { controller = "Home", action = "Index" });
			}
			foreach (var prueba in vm.PrueabaListId) 
			{
				SaveResultadoViewModel resultado = new SaveResultadoViewModel
				{
					PruebaId = prueba,
					PacienteId = vm.PacienteId,
					estado = "Pendiente",
					CitaId = vm.CitaId
				};

				await resultadoServices.Add(resultado);	
			}
			SaveCitaViewModel cita = await _services.GetById(vm.CitaId);
			cita.Estado = "Pendiente de Resultados";
			await _services.Update(cita);
			return RedirectToRoute(new { controller="Cita", action="Index"});
		}

		//---------------------------PendienteResultado-------------------------------------------
		public async Task<IActionResult> PendienteResultados(SaveCitaViewModel cita)  
		{
			if (!_validateUser.HasUser())
			{
				return RedirectToRoute(new { controller = "Home", action = "Index" });
			}
			var ResultadoList = await resultadoServices.GetAllWithInclude();
			ViewBag.CitaId = cita.Id;
			List<ResultadoViewModel> resultadosvm = new();
			foreach (var resultado in ResultadoList) 
			{
				if (resultado.CitaId == cita.Id) 
				{
					resultadosvm.Add(resultado);
				}
			}
			return View(resultadosvm);
		}

		public async Task<IActionResult> Completar(int id) 
		{
			if (!_validateUser.HasUser())
			{
				return RedirectToRoute(new { controller = "Home", action = "Index" });
			}
			SaveCitaViewModel cita = await _services.GetById(id);
			cita.Estado = "Completada";
			await _services.Update(cita);
			return RedirectToRoute(new { controller="Cita", action = "Index"});
		}

		//-------------------------------------VerResultados-------------------------------
		public async Task<IActionResult> VerResultados(SaveCitaViewModel cita) 
		{
			if (!_validateUser.HasUser())
			{
				return RedirectToRoute(new { controller = "Home", action = "Index" });
			}

			var ResultadoList = await resultadoServices.GetAllWithInclude();
			ViewBag.CitaId = cita.Id;
			List<ResultadoViewModel> resultadosvm = new();
			foreach (var resultado in ResultadoList)
			{
				if (resultado.CitaId == cita.Id)
				{
					resultadosvm.Add(resultado);
				}
			}
			return View(resultadosvm);
		}



	}
}
