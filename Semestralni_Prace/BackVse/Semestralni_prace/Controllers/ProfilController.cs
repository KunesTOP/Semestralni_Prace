using Back.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Semestralni_prace.Controllers
{
    public class ProfilController : Controller
    {
       
        public ActionResult Profil()
        {
            var level = AuthController.Check(new AuthToken { PrihlasovaciJmeno = HttpContext.Session.GetString("jmeno") });
            if (level == AuthLevel.NONE) { return RedirectToAction("AutorizaceFailed", "Home"); }
            bool isAdmin = level == AuthLevel.ADMIN;
            var ktereJmenoPouzivat = (isAdmin) ? HttpContext.Session.GetString("emulovaneJmeno") : HttpContext.Session.GetString("jmeno");
            if (isAdmin && ktereJmenoPouzivat != HttpContext.Session.GetString("jmeno")) level = AuthController.GetLevel(ktereJmenoPouzivat);
            return View();
        }

        [HttpPost]
        public IActionResult UpdateProfile(string name, int age, string address, string email, IFormFile picture)
        {
            // Update the user profile with the provided data
            // You can save the updated information to a database or perform any necessary logic here

            // Redirect back to the profile page or wherever you want after the update
            return RedirectToAction("Profil");
        }

    }
}
