using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Inscripciones
{
    public class InscripcionesAddController : Controller
    {
        private readonly IInscripcionesService _inscripcionesService;

        public InscripcionesAddController(IInscripcionesService inscripcionesService)
        {
            _inscripcionesService = inscripcionesService;
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
        public async Task<IActionResult> Add(InscripcionDTO inscripcion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _inscripcionesService.Add(inscripcion);
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