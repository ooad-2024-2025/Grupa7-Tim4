using System.Diagnostics;
using Autosalon_OneZone.Models;
using Microsoft.AspNetCore.Mvc;

namespace Autosalon_OneZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet] // Ovo je GET zahtev kada korisnik klikne na link
        public IActionResult Kontakt()
        {
            // Vraća View fajl koji će se po konvenciji tražiti u Views/Home/Kontakt.cshtml
            // (Uverite se da imate Views/Home/Kontakt.cshtml fajl sa sadržajem koji želite da prikažete)
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
