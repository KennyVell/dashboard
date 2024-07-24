using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Carreras
{
    public class CarrerasEditController : Controller
    {
        private readonly ICarrerasService _carrerasService;

        public CarrerasEditController(ICarrerasService carrerasService)
        {
            _carrerasService = carrerasService;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
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

        [HttpPost]
        public async Task<IActionResult> Update(int id, CarreraDTO carrera)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _carrerasService.Update(id, carrera);
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