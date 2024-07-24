using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Materias
{
    public class MateriasEditController : Controller
    {
        private readonly IMateriasService _materiasService;

        public MateriasEditController(IMateriasService materiasService)
        {
            _materiasService = materiasService;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
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

        [HttpPost]
        public async Task<IActionResult> Update(int id, MateriaDTO materia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _materiasService.Update(id, materia);
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