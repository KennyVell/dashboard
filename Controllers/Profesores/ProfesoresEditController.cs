using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Profesores
{
    public class ProfesoresEditController : Controller
    {
        private readonly IProfesoresService _profesoresService;

        public ProfesoresEditController(IProfesoresService profesoresService)
        {
            _profesoresService = profesoresService;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var profesor = await _profesoresService.GetById(id);
                if (profesor!= null) return View(profesor);
                return NotFound();
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ProfesorDTO profesor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _profesoresService.Update(id, profesor);
                    return RedirectToAction("Index", "Profesores");
                }
                return View(profesor);
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }
    }
}