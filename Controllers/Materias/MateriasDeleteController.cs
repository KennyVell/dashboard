using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Materias
{
    public class MateriasDeleteController : Controller
    {
        private readonly IMateriasService _materiasService;

        public MateriasDeleteController(IMateriasService materiasService)
        {
            _materiasService = materiasService;
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
                var materia = await _materiasService.GetById(id);
                if (materia != null)
                {
                    await _materiasService.Delete(id);
                    return RedirectToAction("Index", "Materias");
                }
                throw new Exception("El materia no existe.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}