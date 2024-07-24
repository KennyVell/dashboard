using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;

namespace dashboard.Controllers.Materias
{
    public class MateriasController : Controller
    {
        private readonly IMateriasService _materiasService;

        public MateriasController(IMateriasService materiasService)
        {
            _materiasService = materiasService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var materias = await _materiasService.GetAll();
                if (materias.Any()) return View(materias);
                return View(materias);
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
                var materia = await _materiasService.GetById(id);
                if (materia!= null) return View(materia);
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