using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Estudiantes
{
    public class EstudiantesDeleteController : Controller
    {
        private readonly IEstudiantesService _estudiantesService;

        public EstudiantesDeleteController(IEstudiantesService estudiantesService)
        {
            _estudiantesService = estudiantesService;
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
                var estudiante = await _estudiantesService.GetById(id);
                if (estudiante != null)
                {
                    await _estudiantesService.Delete(id);
                    return RedirectToAction("Index", "Estudiantes");
                }
                throw new Exception("El estudiante no existe.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}