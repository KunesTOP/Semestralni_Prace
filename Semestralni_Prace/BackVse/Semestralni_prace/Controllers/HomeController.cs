using Microsoft.AspNetCore.Mvc;
using Semestralni_prace.Models;
using Semestralni_prace.Models.Classes;
using Semestralni_prace.Controllers;
using System.Diagnostics;
using Models.DatabaseControllers;
using Back.Auth;
using Semestralni_prace.Models.DatabaseControllers;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Data;

namespace Semestralni_Prace.Controllers
{
    public class HomeController : Controller
    {
        private List<object> list;

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
           /* var level = AuthController.Check(new AuthToken { PrihlasovaciJmeno = HttpContext.Session.GetString("jmeno"), Hash = HttpContext.Session.GetString("heslo") });
            if (level == AuthLevel.NONE) { return RedirectToAction("AutorizaceFailed", "Home"); }
            bool isAdmin = level == AuthLevel.ADMIN;
            var ktereJmenoPouzivat = (isAdmin) ? HttpContext.Session.GetString("emulovaneJmeno") : HttpContext.Session.GetString("jmeno");
            if (isAdmin && ktereJmenoPouzivat != HttpContext.Session.GetString("jmeno")) level = AuthController.GetLevel(ktereJmenoPouzivat);
            if (level == AuthLevel.OUTER) { return RedirectToAction("AutorizaceFailed", "Home"); }
            List<string> tableNames;
            tableNames = (level == AuthLevel.ADMIN) ? GetNazvyTabulekAdmin() : GetNazvyTabulekLekar();*/
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
            return new List<string> {"ADRESY", "ANAMNEZA", "ASISTENT","DOKUMENTY", "LEKARI" ,"LEKY", "MAJITEL","PRUKAZ", "RASA", "TITUL","UCTY", "VAKCINA", "VETERINARNI_KLINIKA",
                "VYSLEDEK_KREV","ZAMESTNANCI","ZVIRE", "PROCEDURY", "FUNKCE", "TRIGGERY", "INDEXY", "SEQUENCE", "CONSTRAINTS", "SYNONYMS","TABLES"};
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
            DataTable table;
            switch (tableName)
            {
                case "ADRESY":
                    return list = new List<object> { AdresyController.GetAll() };
                case "ANAMNEZA":
                    return list = new List<object> { AnamnezyController.GetAll() };
                case "ASISTENT":
                    return list = new List<object> { AsistentiController.GetAll() };
                case "DOKUMENTY":
                    return list = new List<object> { DokumentController.GetAll() };
                case "LEKY":
                    return list = new List<object> { LekyController.GetAll() };
                case "LEKARI":
                    return list = new List<object> { LekariController.GetAll() };
                case "MAJITEL":
                    return list = new List<object> { MajiteleZviratController.GetAll() };
                case "PRUKAZ":
                    return list = new List<object> { PrukazyController.GetAll() };
                case "RASA":
                    return list = new List<object> { RasaZviratController.GetAll() };
                case "TITUL":
                    return list = new List<object> { TitulyController.GetAll() };
                case "UCTY":
                    return list = new List<object> { UctyController.GetAllUcty() };
                case "VAKCINA":
                    return list = new List<object> { VakcinyTableController.GetAll() };
                case "VETERINARNI_KLINIKA":
                    return list = new List<object> { VeterinarniKlinikaController.GetAll() };
                case "VYSLEDEK_KREV":
                    return list = new List<object> { VysledkyKrevController.GetAll() };
                case "ZAMESTNANCI":
                    return list = new List<object> { ZamestnanciController.GetAll() };
                case "ZVIRE":
                    return list = new List<object> { ZvirataController.GetAll() };
                case "PROCEDURY":
                    table = HiearchickyController.GetProcedures();
                    if(table.Rows.Count == 0) { return null; }
                    List<object>listProcedury = new List<object>();
                    foreach(DataRow row in table.Rows)
                    {
                        listProcedury.Add(row[0].ToString());
                    }
                    return list = new List<object> { listProcedury };
                case "FUNKCE":
                    table = HiearchickyController.GetFunctions();
                    if (table.Rows.Count == 0) { return null; }
                    List<object> listFunkce = new List<object>();
                    foreach (DataRow row in table.Rows)
                    {
                        listFunkce.Add(row[0].ToString());
                    }
                    return list = new List<object> { listFunkce };
                case "TRIGGERY":
                    table = HiearchickyController.GetTriggers();
                    if (table.Rows.Count == 0) { return null; }
                    List<object> listTrigger = new List<object>();
                    foreach (DataRow row in table.Rows)
                    {
                        listTrigger.Add(row[0].ToString());
                    }
                    return list = new List<object> { listTrigger };
                case "INDEXY":
                    table = HiearchickyController.GetIndexes();
                    if (table.Rows.Count == 0) { return null; }
                    List<object> listIndex = new List<object>();
                    foreach (DataRow row in table.Rows)
                    {
                        listIndex.Add(row[0].ToString());
                    }
                    return list = new List<object> { listIndex };
                case "SEQUENCE":
                    table = HiearchickyController.GetSequences();
                    if (table.Rows.Count == 0) { return null; }
                    List<object> listSequence = new List<object>();
                    foreach (DataRow row in table.Rows)
                    {
                        listSequence.Add(row[0].ToString());
                    }
                    return list = new List<object> { listSequence };
                case "CONSTRAINTS":
                    table = HiearchickyController.GetConstraints();
                    if (table.Rows.Count == 0) { return null; }
                    List<object> listConstrains = new List<object>();
                    foreach (DataRow row in table.Rows)
                    {
                        listConstrains.Add(row[0].ToString());
                    }
                    return list = new List<object> { listConstrains };
                case "SYNONYMS":
                    table = HiearchickyController.GetSynonyms();
                    if (table.Rows.Count == 0) { return null; }
                    List<object> listSynonyms = new List<object>();
                    foreach (DataRow row in table.Rows)
                    {
                        listSynonyms.Add(row[0].ToString());
                    }
                    return list = new List<object> { listSynonyms };
                case "TABLES":
                    table = HiearchickyController.GetTables();
                    if (table.Rows.Count == 0) { return null; }
                    List<object> listTables = new List<object>();
                    foreach (DataRow row in table.Rows)
                    {
                        listTables.Add(row[0].ToString());
                    }
                    return list = new List<object> { listTables };
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

        [HttpPost]
        public void SaveEditedRow([FromBody] JsonElement input)
        {
            var tabulka = input.GetProperty("selectedValue").GetString();
            var id = input.GetProperty("rowId").GetInt32();
            JsonElement data = input.GetProperty("entries");

            GetSpravnyUpsert(tabulka, id, data);
        }

        [HttpDelete]
        public void DeleteRow([FromBody] JsonElement input)
        {
            var tabulka = input.GetProperty("selectedValue").GetString();
            var id = input.GetProperty("rowId").GetInt32();
            GetSpravnyRemove(tabulka, id);
        }

        private void GetSpravnyUpsert(string selectedValue, int id, JsonElement data)
        {
            switch (selectedValue)
            {
                case "ADRESY":
                    AdresyController.Upsert(id, data);
                    break;
                case "ANAMNEZA":
                    DateTime datumAnamnezy = DateTime.Parse(data.GetProperty("datum").GetString());
                    Anamneza zaznamAnamnezy = new Anamneza()
                    {
                        Id = id,
                        Datum = datumAnamnezy
                    };
                    AnamnezyController.UpdateZaznamAnamnezy(zaznamAnamnezy);
                    break;
                    
                case "ASISTENT":
                    AsistentiController.UpsertAsistent(id, data);
                    break;
                case "DOKUMENTY":
                    var nazev = data.GetProperty("nazev").GetString();
                    var properTities = data.GetProperty("typ").GetString();
                    var obsah = data.GetProperty("data").GetString();
                    Dokument doc = new Dokument
                    {
                        DokumentNazev = nazev,
                        Pripona = Path.GetExtension(nazev),
                        Data = obsah,
                        Id = id
                    };
                    DokumentController.UpsertDokument(doc);
                    break;
                case "LEKARI":
                    LekariController.UpsertLekar(id, data);
                    break;
                case "LEKY":
                    LekyController.UpsertLek(id, data);
                    break;
                case "MAJITEL":
                    MajiteleZviratController.UpsertMajitel(id, data);
                    break;
                case "PRUKAZ":
                    PrukazyController.UpsertPrukaz(id, data);
                    break;
                case "RASA":
                    RasaZviratController.UpsertRasa(id, data);
                    break;
                case "TITUL":
                    TitulyController.UpsertTitul(id, data);
                    break;
                case "UCTY":
                    UctyController.UpdateUcty(id, data);
                    break;
                case "VAKCINA":
                    VakcinyTableController.UpsertVakcina(id, data);
                    break;
                case "VETERINARNI_KLINIKA":
                    VeterinarniKlinikaController.UpsertKlinika(id, data);
                    break;
                case "VYSLEDEK_KREV":
                    VysledkyKrevController.UpsertVysledekKrev(id, data);
                    break;
                case "ZAMESTNANCI":
                    ZamestnanciController.UpsertZamestnanec(id, data);
                    break;
                case "ZVIRE":
                    ZvirataController.UpsertZvire(id, data);
                    break;
            }
        }
        private void GetSpravnyRemove(string selectedValue, int id)
        {
            switch (selectedValue)
            {
                case "ADRESY":
                    AdresyController.Delete(id);
                    break;
                case "ANAMNEZA":

                    //TODO, todle upravit, protože bude třeba řešit i vysledkyKrev
                    AnamnezyController.DeleteZaznamAnamnezy(id);
                    break;
                case "ASISTENT":
                    AsistentiController.DeleteAsistent(id);
                    break;
                case "DOKUMENTY":
                    DokumentController.Delete(id);
                    break;
                case "LEKARI":
                    LekariController.DeleteLekar(id);
                    break;
                case "LEKY":
                    LekyController.DeleteLek(id);
                    break;
                case "MAJITEL":
                    MajiteleZviratController.DeleteMajitel(id);
                    break;
                case "PRUKAZ":
                    PrukazyController.DeletePrukaz(id);
                    break;
                case "RASA":
                    RasaZviratController.DeleteRasa(id);
                    break;
                case "TITUL":
                    TitulyController.DeleteTitul(id);
                    break;
                case "UCTY":
                    UctyController.DeleteUcty(id);
                    break;
                case "VAKCINA":
                    VakcinyTableController.DeleteVakcina(id);
                    break;
                case "VETERINARNI_KLINIKA":
                    VeterinarniKlinikaController.DeleteKlinika(id);
                    break;
                case "VYSLEDEK_KREV":
                    VysledkyKrevController.DeleteVysledekKrev(id);
                    break;
                case "ZAMESTNANCI":
                    ZamestnanciController.DeleteZamestnanec(id);
                    break;
                case "ZVIRE":
                    ZvirataController.DeleteZvire(id);
                    break;
            }
        }
        private int GetSpravneId(string tableName, int id)
        {
            // Todle je z důvodu přidávání... tak defaultně je to ID nastavené na asi -1, tak snad to mám dobře
            if (id <= 0) return -1;    
            switch (tableName)
            {
                case "ADRESY":
                    Adresy adresa = list.OfType<Adresy>().ToList()[id];
                    return adresa.Id;
                case "ANAMNEZA":
                    Anamneza anamneza = list.OfType<Anamneza>().ToList()[id];
                    return anamneza.Id;
                case "ASISTENT":
                    Asistent asistent = list.OfType<Asistent>().ToList()[id];
                    return asistent.Id;
                case "DOKUMENTY":
                    Dokument dokument = list.OfType<Dokument>().ToList()[id];
                    return dokument.Id;
                case "LEKY":
                    Lek lek = list.OfType<Lek>().ToList()[id];
                    return lek.Id;
                case "LEKARI":
                    Lekar lekar = list.OfType<Lekar>().ToList()[id];
                    return lekar.Id;
                case "MAJITEL":
                    Majitel majitel = list.OfType<Majitel>().ToList()[id];
                    return majitel.Id;
                case "PRUKAZ":
                    Prukaz prukaz = list.OfType<Prukaz>().ToList()[id];
                    return prukaz.Id;
                case "RASA":
                    Rasa rasa = list.OfType<Rasa>().ToList()[id];
                    return rasa.Id;
                case "TITUL":
                    Titul titul = list.OfType<Titul>().ToList()[id];
                    return titul.Id;
                case "UCTY":
                    Ucty ucty = list.OfType<Ucty>().ToList()[id];
                    return ucty.Id;
                case "VAKCINA":
                    Vakcina vakcina = list.OfType<Vakcina>().ToList()[id];
                    return vakcina.Id;
                case "VETERINARNI_KLINIKA":
                    VeterinarniKlinika klinika = list.OfType<VeterinarniKlinika>().ToList()[id];
                    return klinika.Id;
                case "VYSLEDEK_KREV":
                    VysledekKrev krev = list.OfType<VysledekKrev>().ToList()[id];
                    return krev.Id;
                case "ZAMESTNANCI":
                    Zamestnanec zamestnanec = list.OfType<Zamestnanec>().ToList()[id];
                    return zamestnanec.Id;
                case "ZVIRE":
                    Zvire zvire = list.OfType<Zvire>().ToList()[id];
                    return zvire.Id;
            }
            return 0;
        }
    }
}