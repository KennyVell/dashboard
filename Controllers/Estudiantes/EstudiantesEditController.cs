using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Estudiantes
{
    public class EstudiantesEditController : Controller
    {
        private readonly IEstudiantesService _estudiantesService;

        public EstudiantesEditController(IEstudiantesService estudiantesService)
        {
            _estudiantesService = estudiantesService;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
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

        [HttpPost]
        public async Task<IActionResult> Update(int id, EstudianteDTO estudiante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _estudiantesService.Update(id, estudiante);
                    return RedirectToAction("Index", "Estudiantes");
                }
                return View(estudiante);
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }
    }
}