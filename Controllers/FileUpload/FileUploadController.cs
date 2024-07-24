using Microsoft.AspNetCore.Mvc;
using dashboard.DTOs;
using dashboard.Interfaces;

namespace YourNamespace.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IFileUploadService _fileUploadService;

        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            return View(new FileUploadDTO());
        }

        [HttpPost]
        public IActionResult UploadFile(FileUploadDTO model)
        {
            if (model.File != null && model.File.Length > 0)
            {
                _fileUploadService.UploadFile(model.File);
            }
            return RedirectToAction("Index");
        }
    }
}
