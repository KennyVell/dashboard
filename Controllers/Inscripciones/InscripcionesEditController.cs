using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Inscripciones
{
    public class InscripcionesEditController : Controller
    {
        private readonly IInscripcionesService _inscripcionesService;

        public InscripcionesEditController(IInscripcionesService inscripcionesService)
        {
            _inscripcionesService = inscripcionesService;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var inscripcion = await _inscripcionesService.GetById(id);
                if (inscripcion!= null) return View(inscripcion);
                return NotFound();
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, InscripcionDTO inscripcion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _inscripcionesService.Update(id, inscripcion);
                    return RedirectToAction("Index", "Inscripciones");
                }
                return View(inscripcion);
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }
    }
}