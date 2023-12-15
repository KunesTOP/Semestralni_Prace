using Back.Auth;
using ConsoleApp1.Models.DatabaseControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models.DatabaseControllers;
using Semestralni_prace.Models.Classes;
using Semestralni_prace.Models.DatabaseControllers;

namespace Semestralni_prace.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            var level = AuthController.Check(new AuthToken { PrihlasovaciJmeno = HttpContext.Session.GetString("jmeno"), Hash = HttpContext.Session.GetString("heslo") });
            if (level == AuthLevel.NONE) { return RedirectToAction("AutorizaceFailed", "Home"); }
            bool isAdmin = level == AuthLevel.ADMIN;
            var ktereJmenoPouzivat = (isAdmin) ? HttpContext.Session.GetString("emulovaneJmeno") : HttpContext.Session.GetString("jmeno");
            if (isAdmin && ktereJmenoPouzivat != HttpContext.Session.GetString("jmeno")) level = AuthController.GetLevel(ktereJmenoPouzivat);
            if (level == AuthLevel.OUTER) { return RedirectToAction("AutorizaceFailed", "Home"); }
            //TODO hodit sem restrikce jen pro admina
            return View();
        }
        public IActionResult RegisterList()
        {
            //TODO tady zavolat všechny prvky
            List<Registrovany> listRegistrovanych = RegisterDBController.GetAllRegisterEntries();
            return View(listRegistrovanych);
        }

        public IActionResult PridatRegistraci([FromBody] Registrovany data)
        {
            if (!ModelState.IsValid)
            {
                //TODO tady by měl být error, ale to nevím jak se momentálně dělá
                return Index();
            }
            RegisterDBController.CreateRegisterEntry(data.Jmeno, data.Prijmeni, data.Email, data.City, data.Street,
                                                    data.HouseNumber.ToString(), data.UserName, PasswordHelper.HashPassword(data.Password));
            return RedirectToAction("Login", "Home");
        }

        public void AddRegister(Registrovany registr)
        {
            if (!registr.Equals(null))
            {
                Zamestnanec novyZam = new Zamestnanec
                {
                    Id = -1,
                    Jmeno = registr.Jmeno,
                    Prijmeni = registr.Prijmeni,
                    Profese = "Asistent",
                    VeterKlinId = (int)VeterinarniKlinikaController.GetKlinikaIdByAdresa(registr.City, registr.Street, registr.HouseNumber)
                };
                Ucty novyUcet = new Ucty
                {
                    Id = -1,
                    Jmeno = registr.UserName,
                    Hash = registr.Password,
                    Uroven = 2
                };
                
                UctyController.CreateUctyBezHashe(novyUcet);
                //TODO tady je třeba ten ZamestnanciController a create zamestnance

                DeleteRegister(registr);
            }

        }

        public void DeleteRegister(Registrovany registr)
        {
            if (!registr.Equals(null))
            {
                RegisterDBController.DeleteRegisterEntry(registr.UserName);
            }
        }

    }
}
