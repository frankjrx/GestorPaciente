using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Pacientes;
using GestorPacientes.MiddleWare;
using Microsoft.AspNetCore.Mvc;

namespace GestorPacientes.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteServices _services;
        private readonly ValidateUserSession _validateUser;

        public PacienteController(IPacienteServices services, ValidateUserSession validateUser)
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

        //------------------------------------Crear-------------------------------------------------
        public IActionResult Create()
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View("SavePaciente", new SavePacienteVIewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePacienteVIewModel vm)
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SavePaciente", vm);
            }

            SavePacienteVIewModel pacientevm = await _services.Add(vm);
            if (pacientevm != null && pacientevm.Id != 0) 
            {
                pacientevm.imgUrl = UploadFile(vm.Foto, pacientevm.Id);
                await _services.Update(pacientevm);
            }
            return RedirectToRoute(new { controller = "Paciente", action = "Index" });
        }

        //-----------------------------------Edit------------------------------------------------
        public async Task<IActionResult> Edit(int id) 
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View("SavePaciente", await _services.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePacienteVIewModel vm) 
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid) 
            {
                return View("SavePaciente", vm);
            }

            SavePacienteVIewModel pacientevm = await _services.GetById(vm.Id);
            vm.imgUrl = UploadFile(pacientevm.Foto, pacientevm.Id, false, pacientevm.imgUrl);
            await _services.Update(vm);
            return RedirectToRoute(new { controller = "Paciente", action = "Index" });
        }

        //----------------------------------------Delete--------------------------------------------------
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
            return RedirectToRoute(new { controller = "Paciente", action = "Index" });
        }


        //----------------------------------Upload File-------------------------------
        private string UploadFile(IFormFile file, int id, bool createMode=true, string imgUrl="") 
        {
            if (!createMode && file==null) 
            {
                return imgUrl;
            }
            string basePath = $"/images/Pacientes/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{basePath}");
            if (!Directory.Exists(path)) 
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileWithPath, FileMode.Create)) 
            {
                file.CopyTo(stream);
            }

            if (!createMode)
            {
                string[] oldImagePart = imgUrl.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}
