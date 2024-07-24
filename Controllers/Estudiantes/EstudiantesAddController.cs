using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Estudiantes
{
    public class EstudiantesAddController : Controller
    {
        private readonly IEstudiantesService _estudiantesService;

        public EstudiantesAddController(IEstudiantesService estudiantesService)
        {
            _estudiantesService = estudiantesService;
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
        public async Task<IActionResult> Add(EstudianteDTO estudiante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _estudiantesService.Add(estudiante);
                    return RedirectToAction("Index", "Estudiantes");
                }
                return View(estudiante);
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }
    }
}