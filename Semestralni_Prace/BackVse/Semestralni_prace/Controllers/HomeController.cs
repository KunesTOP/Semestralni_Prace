using Microsoft.AspNetCore.Mvc;
using Semestralni_prace.Models;
using Semestralni_prace.Models.Classes;
using Semestralni_prace.Controllers;
using System.Diagnostics;
using Models.DatabaseControllers;
using Back.Auth;
using Semestralni_prace.Models.DatabaseControllers;
using Microsoft.CodeAnalysis;

namespace Semestralni_Prace.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Tabulky()
        {
            /* var level = AuthController.Check(new AuthToken { PrihlasovaciJmeno = HttpContext.Session.GetString("jmeno") });
             if (level == AuthLevel.NONE) { return RedirectToAction("AutorizaceFailed", "Home"); }
             bool isAdmin = level == AuthLevel.ADMIN;
             var ktereJmenoPouzivat = (isAdmin) ? HttpContext.Session.GetString("emulovaneJmeno") : HttpContext.Session.GetString("jmeno");
             if (isAdmin && ktereJmenoPouzivat != HttpContext.Session.GetString("jmeno")) level = AuthController.GetLevel(ktereJmenoPouzivat);
             if (level == AuthLevel.OUTER) { return RedirectToAction("AutorizaceFailed", "Home"); }
             List<string> tableNames;
             tableNames = (level == AuthLevel.ADMIN) ? GetNazvyTabulekAdmin() :  GetNazvyTabulekLekar();*/
            List<string> tableNames = GetNazvyTabulekAdmin();
            

            ViewBag.TableNames = tableNames;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<string> GetNazvyTabulekAdmin()
        {
            return new List<string> {"ADRESY", "ANAMNEZA", "ASISTENT","DOKUMENTY", "LEKY", "MAJITEL","PRUKAZ", "RASA", "TITUL","UCTY", "VAKCINA", "VETERINARNI_KLINIKA",
                "VYSLEDEK_KREV","ZAMESTNANCI","ZVIRE"};
        }
        private List<string> GetNazvyTabulekLekar()
        {
            return new List<string> { "LEKY", "MAJITEL", "PRUKAZ", "RASA", "VAKCINA", "VYSLEDEK_KREV", "ZVIRE" };
        }
        public IActionResult LoadTable(string tableName)
        {
            List<object> result = GetSpravnouTabulku(tableName);

            if (result != null)
            {
                return Ok(new { data = result });
            }
            else
            {
                return NotFound();
            }
        }

        private List<object> GetSpravnouTabulku(string tableName)
        {
            switch (tableName)
            {
                case "ADRESY":
                    return new List<object> { AdresyController.GetAll() };
                case "ANAMNEZA":
                    return new List<object> { /*AnamnezyController.GetAll2()*/ };
                case "ASISTENT":
                    return new List<object> { AsistentiController.GetAll() };
                case "DOKUMENTY":
                    return new List<object> { DokumentController.GetAll() };
                case "LEKY":
                    return new List<object> { LekyController.GetAll() };
                case "MAJITEL":
                    return new List<object> { MajiteleZviratController.GetAll() };
                case "PRUKAZ":
                    return new List<object> { PrukazyController.GetAll() };
                case "RASA":
                    return new List<object> { RasaZviratController.GetAll() };
                case "TITUL":
                    return new List<object> { TitulyController.GetAll() };
                case "UCTY":
                    return new List<object> { UctyController.GetAll() };
                case "VAKCINA":
                    return new List<object> { VakcinyTableController.GetAll() };
                case "VETERINARNI_KLINIKA":
                    return new List<object> { VeterinarniKlinikaController.GetAll() };
                case "VYSLEDEK_KREV":
                    return new List<object> { VysledkyKrevController.GetAll() };
                case "ZAMESTNANCI":
                    return new List<object> { ZamestnanciController.GetAll() };
                case "ZVIRE":
                    return new List<object> { ZvirataController.GetAll() };
            }
            return null;
        }
        [HttpPost]
        public IActionResult Login(string loginName, string loginPassword)
        {
            if (AuthController.Check(new AuthToken { PrihlasovaciJmeno = loginName, Hash = loginPassword }) == AuthLevel.NONE)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                ViewData["ErrorMessage"] = "Invalid login attempt. ";
                return View();
            }
            HttpContext.Session.SetString("jmeno", loginName);
            HttpContext.Session.SetString("heslo", loginPassword);
            HttpContext.Session.SetString("emulovaneJmeno", loginName);


            return RedirectToAction("Profil", "Profil");
        }

        public IActionResult AutorizaceFailed()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Home", "Login");
        }

    }
}