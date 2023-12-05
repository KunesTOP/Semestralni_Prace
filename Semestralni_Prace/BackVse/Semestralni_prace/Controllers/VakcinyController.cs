using Back.Auth;
using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Models.DatabaseControllers;
using Semestralni_prace.Models.Classes;

namespace Semestralni_prace.Controllers
{
    public class VakcinyController : Controller
    {
        public IActionResult ListVakcin()
        {
            var level = AuthController.Check(new AuthToken { PrihlasovaciJmeno = HttpContext.Session.GetString("jmeno") });
            if (level == AuthLevel.NONE) { return RedirectToAction("AutorizaceFailed", "Home"); }
            bool isAdmin = level == AuthLevel.ADMIN;
            var ktereJmenoPouzivat = (isAdmin) ? HttpContext.Session.GetString("emulovaneJmeno") : HttpContext.Session.GetString("jmeno");
            if (isAdmin && ktereJmenoPouzivat != HttpContext.Session.GetString("jmeno")) level = AuthController.GetLevel(ktereJmenoPouzivat);
            if(level == AuthLevel.OUTER) { return RedirectToAction("AutorizaceFailed", "Home"); }
            return View(VakcinyTableController.GetAsistentPodaVakcinuZviretiView());
        }
    }
}
