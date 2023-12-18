using Back.Auth;
using Microsoft.AspNetCore.Mvc;
using Models.DatabaseControllers;
using Semestralni_prace.Models.Classes;
using Semestralni_prace.Models.DatabaseControllers;

namespace Semestralni_prace.Controllers
{
    public class HierarchicalController : Controller
    {
        public class Zviratka : Zvire
        {
            public int Vek { get; set; }

        }
        public IActionResult hierarchicky()
        {
            var level = AuthController.Check(new AuthToken { PrihlasovaciJmeno = HttpContext.Session.GetString("jmeno"), Hash = HttpContext.Session.GetString("heslo") });
            if (level == AuthLevel.NONE) { return RedirectToAction("AutorizaceFailed", "Home"); }
            bool isAdmin = level == AuthLevel.ADMIN;
            var ktereJmenoPouzivat = (isAdmin) ? HttpContext.Session.GetString("emulovaneJmeno") : HttpContext.Session.GetString("jmeno");
            if (isAdmin && ktereJmenoPouzivat != HttpContext.Session.GetString("jmeno")) level = AuthController.GetLevel(ktereJmenoPouzivat);

            return View(VratListSVekem());
        }
        //TODO Todle absolutně netuším k čemu má být... just saying
        public IActionResult GetAnimalsForOwner(int ownerId)
        {
            try
            {
                HiearchickyController.GetAnimalsForOwner(ownerId);
                // Zde můžete přidat logiku pro zpracování výsledků
                return View("Success"); // Nebo přesměrování na jiné view
            }
            catch (Exception ex)
            {
                // Zpracování chyb
                return View("Error", ex.Message);
            }
        }
        private List<Zviratka> VratListSVekem()
        {
            List<Zvire> listZvirat = ZvirataController.GetAll();
            List<Zviratka> listZviratekSVěkem = new List<Zviratka>();

            foreach (Zvire zvire in listZvirat)
            {
                listZviratekSVěkem.Add(new Zviratka
                {
                    Id = zvire.Id,
                    DatumNarozeni = zvire.DatumNarozeni,
                    DatumUmrti = zvire.DatumUmrti,
                    JmenoZvire = zvire.JmenoZvire,
                    MajitelZvireIdPacient = zvire.MajitelZvireIdPacient,
                    Pohlavi = zvire.Pohlavi,
                    RasaZviratIdRasa = zvire.RasaZviratIdRasa,
                    Vek = HiearchickyController.CalculateAge(zvire.DatumNarozeni)
                });
            }
            return listZviratekSVěkem;
        }
    }
}
