using Microsoft.AspNetCore.Mvc;
using dashboard.Interfaces;
using dashboard.DTOs;

namespace dashboard.Controllers.Universidades
{
    public class UniversidadesAddController : Controller
    {
        private readonly IUniversidadesService _universidadesService;

        public UniversidadesAddController(IUniversidadesService universidadesService)
        {
            _universidadesService = universidadesService;
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
        public async Task<IActionResult> Add(UniversidadDTO universidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _universidadesService.Add(universidad);
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