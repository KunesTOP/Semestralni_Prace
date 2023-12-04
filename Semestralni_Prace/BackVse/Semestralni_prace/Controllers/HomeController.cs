using Microsoft.AspNetCore.Mvc;
using Semestralni_prace.Models;
using Semestralni_prace.Models.Classes;
using Semestralni_prace.Controllers;
using System.Diagnostics;
using Models.DatabaseControllers;

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
            List<string> tableNames = GetNazvyTabulek();

            ViewBag.TableNames = tableNames;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<string> GetNazvyTabulek()
        {
            return new List<string> {"ADRESY", "ANAMNEZA", "ASISTENT", "LEKY", "MAJITEL","PRUKAZ", "RASA", "TITUL", "VAKCINA", "VETERINARNI_KLINIKA",
                "VYSLEDEK_KREV","ZAMESTNANCI","ZVIRE"};
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
                    return new List<object> { AnamnezyController.GetAll2() };
                case "ASISTENT":
                    return new List<object> { AsistentiController.GetAll() };
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

    }
    /*[HttpPost]
    public IActionResult UpdateRow(int id, string columnName1, string columnName2)
    {
        // Logic to update the row in the database based on the provided values
        // You'll need to implement this logic

        // Assuming the update was successful
        return Json(new { success = true });
    }*/
}