using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;

namespace dashboard.Controllers.Carreras
{
    public class CarrerasController : Controller
    {
        private readonly ICarrerasService _carrerasService;

        public CarrerasController(ICarrerasService carrerasService)
        {
            _carrerasService = carrerasService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var carreras = await _carrerasService.GetAll();
                if (carreras.Any()) return View(carreras);
                return View(carreras);
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
                var carrera = await _carrerasService.GetById(id);
                if (carrera!= null) return View(carrera);
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