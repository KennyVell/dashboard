using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;

namespace dashboard.Controllers.Universidades
{
    public class UniversidadesController : Controller
    {
        private readonly IUniversidadesService _universidadesService;

        public UniversidadesController(IUniversidadesService universidadesService)
        {
            _universidadesService = universidadesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var universidades = await _universidadesService.GetAll();
                if (universidades.Any()) return View(universidades);
                return View(universidades);
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
                var universidad = await _universidadesService.GetById(id);
                if (universidad!= null) return View(universidad);
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