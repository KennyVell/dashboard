using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Inscripciones
{
    public class InscripcionesDeleteController : Controller
    {
        private readonly IInscripcionesService _inscripcionesService;

        public InscripcionesDeleteController(IInscripcionesService inscripcionesService)
        {
            _inscripcionesService = inscripcionesService;
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
                var inscripcion = await _inscripcionesService.GetById(id);
                if (inscripcion != null)
                {
                    await _inscripcionesService.Delete(id);
                    return RedirectToAction("Index", "Inscripciones");
                }
                throw new Exception("El inscripcion no existe.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}