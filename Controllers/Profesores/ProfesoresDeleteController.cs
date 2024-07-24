using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Profesores
{
    public class ProfesoresDeleteController : Controller
    {
        private readonly IProfesoresService _profesoresService;

        public ProfesoresDeleteController(IProfesoresService profesoresService)
        {
            _profesoresService = profesoresService;
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
                var profesor = await _profesoresService.GetById(id);
                if (profesor != null)
                {
                    await _profesoresService.Delete(id);
                    return RedirectToAction("Index", "Profesores");
                }
                throw new Exception("El profesor no existe.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}