using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Profesores
{
    public class ProfesoresAddController : Controller
    {
        private readonly IProfesoresService _profesoresService;

        public ProfesoresAddController(IProfesoresService profesoresService)
        {
            _profesoresService = profesoresService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProfesorDTO profesor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _profesoresService.Add(profesor);
                    return RedirectToAction("Index", "Profesores");
                }
                return View(profesor);
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }
    }
}