using Back.Auth;
using Microsoft.AspNetCore.Mvc;
using Models.DatabaseControllers;
using Semestralni_prace.Models.Classes;
using Semestralni_prace.Models.DatabaseControllers;
using System.Runtime.CompilerServices;

namespace Semestralni_prace.Controllers
{
    public class KlinikyController : Controller
    {
        List<Adresy> tableNames = (List<Adresy>)AdresyController.GetAll();
        List<Zamestnanec> listZamestnancu = ZamestnanciController.GetAll();

        public IActionResult ZamestnanciList()
        {
            /*var level = AuthController.Check(new AuthToken { PrihlasovaciJmeno = HttpContext.Session.GetString("jmeno"), Hash = HttpContext.Session.GetString("heslo") });
            if (level == AuthLevel.NONE) { return RedirectToAction("AutorizaceFailed", "Home"); }
            bool isAdmin = level == AuthLevel.ADMIN;
            var ktereJmenoPouzivat = (isAdmin) ? HttpContext.Session.GetString("emulovaneJmeno") : HttpContext.Session.GetString("jmeno");
            if (isAdmin && ktereJmenoPouzivat != HttpContext.Session.GetString("jmeno")) level = AuthController.GetLevel(ktereJmenoPouzivat);*/

            List<string> listMest = new List<string>();
            foreach(Adresy ad in tableNames)
            {
                listMest.Add(ad.City);
            }
            ViewBag.TableNames = listMest;
            return View();
        }

        [HttpGet]
        public IActionResult LoadTable(string tableName)
        {
            //TODO:Dopsat a zkontrolvoat tudle logiku: Vezmu si ID klinik. Podle toho si najdu adresy a budu vypisovat jen Mesta.
            Adresy spravnaAdresa = tableNames.FirstOrDefault(x => x.City == tableName);
            int? spravnaKlinika = VeterinarniKlinikaController.GetKlinikaIdByAdresa(spravnaAdresa.City,spravnaAdresa.Street,spravnaAdresa.HouseNumber);

            List<Zamestnanec> result = HiearchickyController.NajdiZamestnancePodleKliniky(spravnaKlinika);

            if (result != null)
            {
                return Ok(new { data = result });
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult ZamestnanciNadrizeni()
        {
            List<string> listNadrizenychPrijmeni = new List<string>();
            List<Zamestnanec> listZamestnancu = ZamestnanciController.GetAll();
            listZamestnancu = listZamestnancu.FindAll(x => x.Profese == "Lekar");

            foreach (Zamestnanec zam in listZamestnancu)
            {
                listNadrizenychPrijmeni.Add(zam.Prijmeni);
            }
            ViewBag.TableNames = listNadrizenychPrijmeni;

            return View();
        }
        public IActionResult LoadTableNadrizeni(string tableName)
        {
            Zamestnanec vybranyNadrizeny = listZamestnancu.FirstOrDefault(x => x.Prijmeni == tableName);

            List<HiearchieZamestnancu> result = HiearchickyController.GetAllPodrizeni(vybranyNadrizeny.Id);

            return Ok(new { vybranyNadrizeny });
        }
    }
}
