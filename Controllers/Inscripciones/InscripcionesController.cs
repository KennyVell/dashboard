using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;

namespace dashboard.Controllers.Inscripciones
{
    public class InscripcionesController : Controller
    {
        private readonly IInscripcionesService _inscripcionesService;

        public InscripcionesController(IInscripcionesService inscripcionesService)
        {
            _inscripcionesService = inscripcionesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var inscripciones = await _inscripcionesService.GetAll();
                if (inscripciones.Any()) return View(inscripciones);
                return View(inscripciones);
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
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
    }
}