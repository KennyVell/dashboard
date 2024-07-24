using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Materias
{
    public class MateriasAddController : Controller
    {
        private readonly IMateriasService _materiasService;

        public MateriasAddController(IMateriasService materiasService)
        {
            _materiasService = materiasService;
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
        public async Task<IActionResult> Add(MateriaDTO materia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _materiasService.Add(materia);
                    return RedirectToAction("Index", "Materias");
                }
                return View(materia);
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }
    }
}