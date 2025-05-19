// Put this file in your Controllers folder, e.g., Autosalon_OneZone/Controllers/VoziloController.cs
// OVO JE ZA PRIKAZ DETALJA VOZILA
using Microsoft.AspNetCore.Mvc;
using Autosalon_OneZone.Services; // Potrebno za IVoziloService
using System.Threading.Tasks; // Potrebno za async/await
using System.Collections.Generic; // Potrebno za IEnumerable
using Autosalon_OneZone.Models; // Potrebno za Vozilo model i ViewModele (ako se koriste za Create/Edit)
using Microsoft.AspNetCore.Authorization; // Potrebno za [Authorize]

namespace Autosalon_OneZone.Controllers
{
    public class VoziloController : Controller
    {
        private readonly IVoziloService _voziloService;

        // Injektovanje IVoziloService kroz konstruktor
        public VoziloController(IVoziloService voziloService)
        {
            _voziloService = voziloService;
        }

        // GET: /Vozilo/Index
        // Akcija za prikaz liste svih vozila
        // Dostupna SVIM korisnicima (ukljucujuci goste) jer nema [Authorize] atributa
        public async Task<IActionResult> Index()
        {
            // Pozivamo servis da dohvati sva vozila iz baze
            var vozila = await _voziloService.GetAllVozilaAsync();

            // Prosleđujemo listu vozila View-u (Views/Vozilo/Index.cshtml)
            return View(vozila);
        }

        // --- Akcije za ADMINISTRACIJU VOZILA (samo za rolu Administrator) ---

        // GET: /Vozilo/Create
        // Akcija za prikaz forme za dodavanje novog vozila
        [Authorize(Policy = "RequireAdminRole")] // Samo Admini mogu videti ovu formu
        [HttpGet]
        public IActionResult Create()
        {
            // Vraćamo View koji sadrži formu za unos podataka novog vozila (Views/Vozilo/Create.cshtml)
            return View();
        }

        // POST: /Vozilo/Create
        // Akcija za procesiranje forme i dodavanje novog vozila u bazu
        [Authorize(Policy = "RequireAdminRole")] // Samo Admini mogu izvršiti ovu akciju
        [HttpPost]
        [ValidateAntiForgeryToken] // Zaštita od CSRF napada
        public async Task<IActionResult> Create(Vozilo novoVozilo) // Model Binding: ASP.NET Core mapira podatke iz forme na Vozilo objekat
        {
            // Proveravamo server-side validaciju (na osnovu Data Annotations u Vozilo modelu)
            if (ModelState.IsValid)
            {
                // Pozivamo servis da doda novo vozilo u bazu
                var addedVozilo = await _voziloService.AddVoziloAsync(novoVozilo);

                // Nakon uspešnog dodavanja, preusmeravamo korisnika nazad na listu vozila
                // nameof(Index) se koristi umesto stringa "Index" radi sigurnosti (ako preimenujete akciju)
                return RedirectToAction(nameof(Index));
            }

            // Ako validacija ne prođe, vraćamo istu formu sa popunjenim podacima i greškama validacije
            return View(novoVozilo);
        }


        // --- PRIMERI AKCIJA ZA BUDUĆU IMPLEMENTACIJU (Trenutno KOMENTARISANE) ---

        // GET: /Vozilo/Details/5
        // Akcija za prikaz detalja o jednom vozilu
        // [HttpGet] // Opciono: moze biti dostupno svima ili samo prijavljenim
        // public async Task<IActionResult> Details(int id)
        // {
        //     // ... implementacija ...
        //     return View(); // Ili NotFound()
        // }

        // GET: /Vozilo/Edit/5
        // Akcija za prikaz forme za izmenu postojećeg vozila
        // [Authorize(Policy = "RequireAdminRole")]
        // [HttpGet]
        // public async Task<IActionResult> Edit(int id)
        // {
        //     // ... implementacija ...
        //     return View(); // Ili NotFound()
        // }

        // POST: /Vozilo/Edit/5
        // Akcija za procesiranje forme i ažuriranje postojećeg vozila
        // [Authorize(Policy = "RequireAdminRole")]
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, Vozilo updatedVozilo)
        // {
        //     // ... implementacija ...
        //     return RedirectToAction(nameof(Index)); // Ili neku drugu akciju
        // }

        // POST: /Vozilo/Delete/5
        // Akcija za brisanje vozila iz baze (često ide sa GET potvrdom brisanja)
        // [Authorize(Policy = "RequireAdminRole")]
        // [HttpPost, ActionName("Delete")] // Ime akcije je DeleteConfirmed, ali putanja je /Vozilo/Delete/5
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     // ... implementacija ...
        //     return RedirectToAction(nameof(Index));
        // }

        // GET: /Vozilo/FilteredList?marka=BMW&godisteOd=2020
        // Akcija za prikaz filtrirane/pretražene liste vozila
        // [HttpGet]
        // public async Task<IActionResult> FilteredList(string marka, string model, ...)
        // {
        //     // ... implementacija ...
        //     return View("Index", vozila); // Prikazuje isti View, ali sa filtriranim/pretraženim rezultatima
        // }

    }
}