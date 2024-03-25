using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Medicos;
using GestorPacientes.Core.Application.ViewModels.Pacientes;
using GestorPacientes.MiddleWare;
using Microsoft.AspNetCore.Mvc;

namespace GestorPacientes.Controllers
{
    public class MedicoController : Controller
    {
        private readonly IMedicoServices _services;
        private readonly ValidateUserSession _validateUser;
        public MedicoController(IMedicoServices services, ValidateUserSession validateUser)
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
            var medicos = await _services.GetAll();
            return View(medicos);
        }

        //----------------------------------------------------Crear Medico---------------------------------------------
        public IActionResult Create() 
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View("SaveMedico", new SaveMedicoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveMedicoViewModel vm) 
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid) 
            {
                return View("SaveMedico", vm);
            }

            SaveMedicoViewModel medicovm = await _services.Add(vm);

            if (medicovm != null && medicovm.Id != 0)
            {
                medicovm.imgUrl = UploadFile(vm.Foto, medicovm.Id);
                await _services.Update(medicovm);
            }
            return RedirectToRoute(new { controller = "Medico", action = "Index" });

        }

        //--------------------------------------------------------Editar Medico--------------------------------------------------
        public async Task<IActionResult> Edit(int id) 
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var medico = await _services.GetById(id);
            return View("SaveMedico", medico);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveMedicoViewModel vm) 
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid) 
            {
                return View("SaveMedico", vm);
            }
            SaveMedicoViewModel medicovm = await _services.GetById(vm.Id);
            vm.imgUrl = UploadFile(medicovm.Foto, medicovm.Id, false, medicovm.imgUrl);
            await _services.Update(vm);
            return RedirectToRoute(new { controller = "Medico", action = "Index" });
        }

        //----------------------------------------------------------Eliminar Medico---------------------------------------------
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
            return RedirectToRoute(new { controller = "Medico", action = "Index" });
        }

        //----------------------------------Upload File-------------------------------
        private string UploadFile(IFormFile file, int id, bool createMode = true, string imgUrl = "")
        {
            if (!createMode && file == null)
            {
                return imgUrl;
            }
            string basePath = $"/images/Medicos/{id}";
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
