// Controllers/KorpaController.cs
using Autosalon_OneZone.Helpers;
using Autosalon_OneZone.Models;
using Autosalon_OneZone.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Autosalon_OneZone.Models.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Autosalon_OneZone.Controllers
{
    [Authorize] // Zahtijeva autentikaciju za sve akcije u kontroleru
    public class KorpaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<KorpaController> _logger;

        public KorpaController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<KorpaController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DodajUKorpu(int id)
        {
            try
            {
                _logger.LogInformation($"Početak dodavanja vozila ID: {id} u korpu");

                // Pronađi vozilo u bazi
                var vozilo = await _context.Vozila.FindAsync(id);
                if (vozilo == null)
                {
                    _logger.LogWarning($"Vozilo sa ID: {id} nije pronađeno");
                    TempData["ErrorMessage"] = "Vozilo nije pronađeno.";
                    return Redirect(Request.Headers["Referer"].ToString() ?? "/Vozilo");
                }

                decimal cijenaVozila = vozilo.Cijena ?? 0;
                _logger.LogInformation($"Cijena vozila: {cijenaVozila}");

                // Dohvati trenutno prijavljenog korisnika
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    // Ova linija se ne bi trebala izvršiti zbog [Authorize] atributa,
                    // ali dodatna je provjera
                    _logger.LogWarning("Korisnik nije prijavljen iako je zaštićeno sa [Authorize]");
                    return RedirectToAction("Login", "Account",
                        new { returnUrl = Url.Action("Index", "Vozilo") });
                }

                _logger.LogInformation($"Korisnik je prijavljen, ID: {user.Id}");

                // Prvo provjerimo ima li korisnik već korpu
                var korpa = await _context.Korpe
                    .Include(k => k.StavkeKorpe)
                    .FirstOrDefaultAsync(k => k.KorisnikId == user.Id);

                if (korpa == null)
                {
                    _logger.LogInformation("Korisnik nema korpu, kreiram novu");

                    // Kreiraj novu korpu za korisnika
                    korpa = new Korpa
                    {
                        KorisnikId = user.Id,
                        UkupnaCijena = 0 // Inicijalna ukupna cijena je 0
                    };

                    _context.Korpe.Add(korpa);
                    await _context.SaveChangesAsync();

                    // Ponovno dohvati korpu sa ID-em koji je generirala baza
                    korpa = await _context.Korpe.FirstOrDefaultAsync(k => k.KorisnikId == user.Id);
                    _logger.LogInformation($"Kreirana nova korpa, ID: {korpa.KorpaID}");
                }

                // Provjeri ima li već vozilo u korpi
                var stavkaKorpe = await _context.StavkeKorpe
                    .FirstOrDefaultAsync(s => s.KorpaID == korpa.KorpaID && s.VoziloID == id);

                if (stavkaKorpe == null)
                {
                    _logger.LogInformation($"Dodajem novo vozilo u korpu ID: {korpa.KorpaID}");

                    // Dodaj novu stavku u korpu
                    var novaStavka = new StavkaKorpe
                    {
                        KorpaID = korpa.KorpaID,
                        VoziloID = id,
                        Kolicina = 1,
                        CijenaStavke = cijenaVozila
                    };

                    _context.StavkeKorpe.Add(novaStavka);

                    // Ažuriraj ukupnu cijenu korpe
                    korpa.UkupnaCijena += cijenaVozila;
                    _context.Korpe.Update(korpa);

                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Vozilo dodano u korpu, nova ukupna cijena: {korpa.UkupnaCijena}");

                    TempData["SuccessMessage"] = "Vozilo je uspješno dodato u korpu!";
                }
                else
                {
                    _logger.LogInformation("Vozilo je već u korpi");
                    TempData["InfoMessage"] = "Vozilo je već dodato u korpu.";
                }

                return Redirect(Request.Headers["Referer"].ToString() ?? "/Vozilo");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Greška pri dodavanju vozila u korpu: {ex.Message}");
                TempData["ErrorMessage"] = "Došlo je do greške pri dodavanju vozila u korpu. Molimo pokušajte ponovo.";
                return Redirect(Request.Headers["Referer"].ToString() ?? "/Vozilo");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                List<CartItemViewModel> stavkeKorpe = new List<CartItemViewModel>();
                decimal ukupnaCijena = 0;

                // Za prijavljenog korisnika, dohvati korpu iz baze
                var korpa = await _context.Korpe
                    .Include(k => k.StavkeKorpe)
                    .ThenInclude(s => s.Vozilo)
                    .FirstOrDefaultAsync(k => k.KorisnikId == user.Id);

                if (korpa != null && korpa.StavkeKorpe != null)
                {
                    _logger.LogInformation($"Pronađena korpa sa {korpa.StavkeKorpe.Count} stavki");

                    stavkeKorpe = korpa.StavkeKorpe.Select(s => new CartItemViewModel
                    {
                        Id = s.VoziloID,
                        StavkaId = s.StavkaID,
                        Naziv = $"{s.Vozilo.Marka} {s.Vozilo.Model}",
                        SlikaUrl = !string.IsNullOrEmpty(s.Vozilo.Slika) ? $"/images/vozila/{s.Vozilo.Slika}" : "/img/no-image.png",
                        Godiste = s.Vozilo.Godiste ?? 0,
                        Gorivo = s.Vozilo.Gorivo.ToString(),
                        Cijena = s.CijenaStavke,
                        Kolicina = s.Kolicina
                    }).ToList();

                    ukupnaCijena = korpa.UkupnaCijena;
                }

                var model = new CartViewModel
                {
                    VozilaUKorpi = stavkeKorpe,
                    UkupnaCijena = ukupnaCijena
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Greška pri prikazivanju korpe: {ex.Message}");
                TempData["ErrorMessage"] = "Došlo je do greške pri prikazivanju korpe. Molimo pokušajte ponovo.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UkloniIzKorpe(int id)
        {
            try
            {
                _logger.LogInformation($"Uklanjanje stavke iz korpe, ID: {id}");
                var user = await _userManager.GetUserAsync(User);

                // Za prijavljenog korisnika, briši iz baze
                var korpa = await _context.Korpe
                    .FirstOrDefaultAsync(k => k.KorisnikId == user.Id);

                if (korpa != null)
                {
                    var stavka = await _context.StavkeKorpe
                        .FirstOrDefaultAsync(s => s.VoziloID == id && s.KorpaID == korpa.KorpaID);

                    if (stavka != null)
                    {
                        _logger.LogInformation($"Pronađena stavka za brisanje, ID: {stavka.StavkaID}, Cijena: {stavka.CijenaStavke}");

                        // Umanjujemo ukupnu cijenu korpe
                        korpa.UkupnaCijena -= (stavka.CijenaStavke * stavka.Kolicina);
                        if (korpa.UkupnaCijena < 0) korpa.UkupnaCijena = 0; // Sigurnosna provjera

                        _context.Korpe.Update(korpa);
                        _context.StavkeKorpe.Remove(stavka);

                        await _context.SaveChangesAsync();
                        _logger.LogInformation($"Stavka uklonjena, nova ukupna cijena: {korpa.UkupnaCijena}");

                        TempData["SuccessMessage"] = "Vozilo je uklonjeno iz korpe.";
                    }
                    else
                    {
                        _logger.LogWarning($"Stavka nije pronađena za VoziloID: {id} u KorpaID: {korpa.KorpaID}");
                        TempData["InfoMessage"] = "Vozilo nije pronađeno u korpi.";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Greška pri uklanjanju vozila iz korpe: {ex.Message}");
                TempData["ErrorMessage"] = "Došlo je do greške pri uklanjanju vozila iz korpe. Molimo pokušajte ponovo.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AzurirajKolicinu(int stavkaId, int kolicina)
        {
            try
            {
                if (kolicina < 1)
                {
                    TempData["ErrorMessage"] = "Količina ne može biti manja od 1.";
                    return RedirectToAction("Index");
                }

                var user = await _userManager.GetUserAsync(User);
                var korpa = await _context.Korpe
                    .FirstOrDefaultAsync(k => k.KorisnikId == user.Id);

                if (korpa != null)
                {
                    var stavka = await _context.StavkeKorpe
                        .FirstOrDefaultAsync(s => s.StavkaID == stavkaId && s.KorpaID == korpa.KorpaID);

                    if (stavka != null)
                    {
                        _logger.LogInformation($"Ažuriranje količine stavke ID: {stavkaId}, stara kol: {stavka.Kolicina}, nova kol: {kolicina}");

                        // Računamo razliku za ukupnu cijenu
                        decimal staraVrijednost = stavka.Kolicina * stavka.CijenaStavke;
                        decimal novaVrijednost = kolicina * stavka.CijenaStavke;

                        // Ažuriraj količinu stavke
                        stavka.Kolicina = kolicina;
                        _context.StavkeKorpe.Update(stavka);

                        // Ažuriraj ukupnu cijenu korpe
                        korpa.UkupnaCijena = korpa.UkupnaCijena - staraVrijednost + novaVrijednost;
                        _context.Korpe.Update(korpa);

                        await _context.SaveChangesAsync();

                        TempData["SuccessMessage"] = "Količina je ažurirana.";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Greška pri ažuriranju količine: {ex.Message}");
                TempData["ErrorMessage"] = "Došlo je do greške pri ažuriranju količine.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            try
            {
                // Checkout je dostupan samo prijavljenim korisnicima
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Greška pri prikazivanju checkout stranice: {ex.Message}");
                TempData["ErrorMessage"] = "Došlo je do greške. Molimo pokušajte ponovo.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OcistiKorpu()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var korpa = await _context.Korpe
                    .Include(k => k.StavkeKorpe)
                    .FirstOrDefaultAsync(k => k.KorisnikId == user.Id);

                if (korpa != null)
                {
                    // Ukloni sve stavke iz korpe
                    _context.StavkeKorpe.RemoveRange(korpa.StavkeKorpe);

                    // Resetiraj ukupnu cijenu
                    korpa.UkupnaCijena = 0;
                    _context.Korpe.Update(korpa);

                    await _context.SaveChangesAsync();
                }

                TempData["SuccessMessage"] = "Korpa je uspješno očišćena.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Greška pri čišćenju korpe: {ex.Message}");
                TempData["ErrorMessage"] = "Došlo je do greške pri čišćenju korpe.";
                return RedirectToAction("Index");
            }
        }
    }
}
