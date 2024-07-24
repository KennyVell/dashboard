using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Universidades
{
    public class UniversidadesDeleteController : Controller
    {
        private readonly IUniversidadesService _universidadesService;

        public UniversidadesDeleteController(IUniversidadesService universidadesService)
        {
            _universidadesService = universidadesService;
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
                var universidad = await _universidadesService.GetById(id);
                if (universidad != null)
                {
                    await _universidadesService.Delete(id);
                    return RedirectToAction("Index", "Universidades");
                }
                throw new Exception("El universidad no existe.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}