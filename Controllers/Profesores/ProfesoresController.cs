using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;

namespace dashboard.Controllers.Profesores
{
    public class ProfesoresController : Controller
    {
        private readonly IProfesoresService _profesoresService;

        public ProfesoresController(IProfesoresService profesoresService)
        {
            _profesoresService = profesoresService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var profesores = await _profesoresService.GetAll();
                if (profesores.Any()) return View(profesores);
                return View(profesores);
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
                var profesor = await _profesoresService.GetById(id);
                if (profesor!= null) return View(profesor);
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