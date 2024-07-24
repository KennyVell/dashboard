using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Universidades
{
    public class UniversidadesEditController : Controller
    {
        private readonly IUniversidadesService _universidadesService;

        public UniversidadesEditController(IUniversidadesService universidadesService)
        {
            _universidadesService = universidadesService;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var universidad = await _universidadesService.GetById(id);
                if (universidad!= null) return View(universidad);
                return NotFound();
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UniversidadDTO universidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _universidadesService.Update(id, universidad);
                    return RedirectToAction("Index", "Universidades");
                }
                return View(universidad);
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);                
            }
        }
    }
}