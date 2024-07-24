using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;

namespace dashboard.Controllers.Estudiantes
{
    public class EstudiantesController : Controller
    {
        private readonly IEstudiantesService _estudiantesService;

        public EstudiantesController(IEstudiantesService estudiantesService)
        {
            _estudiantesService = estudiantesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var estudiantes = await _estudiantesService.GetAll();
                if (estudiantes.Any()) return View(estudiantes);
                return View(estudiantes);
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var estudiante = await _estudiantesService.GetById(id);
                if (estudiante!= null) return View(estudiante);
                return NotFound();
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }
    }
}