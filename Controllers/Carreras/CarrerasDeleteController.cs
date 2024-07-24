using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Carreras
{
    public class CarrerasDeleteController : Controller
    {
        private readonly ICarrerasService _carrerasService;

        public CarrerasDeleteController(ICarrerasService carrerasService)
        {
            _carrerasService = carrerasService;
        }

        [HttpGet]
        public IActionResult Delete()
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var carrera = await _carrerasService.GetById(id);
                if (carrera != null)
                {
                    await _carrerasService.Delete(id);
                    return RedirectToAction("Index", "Carreras");
                }
                throw new Exception("El carrera no existe.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}