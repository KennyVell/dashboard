using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Carreras
{
    public class CarrerasAddController : Controller
    {
        private readonly ICarrerasService _carrerasService;

        public CarrerasAddController(ICarrerasService carrerasService)
        {
            _carrerasService = carrerasService;
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
        public async Task<IActionResult> Add(CarreraDTO carrera)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _carrerasService.Add(carrera);
                    return RedirectToAction("Index", "Carreras");
                }
                return View(carrera);
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }
    }
}