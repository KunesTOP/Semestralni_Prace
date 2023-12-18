using Back.Auth;
using ConsoleApp1.Models.DatabaseControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models.DatabaseControllers;
using Semestralni_prace.Models.Classes;
using Semestralni_prace.Models.DatabaseControllers;
using System.Text.Json;

namespace Semestralni_prace.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult RegisterList()
        {
            /*var level = AuthController.Check(new AuthToken { PrihlasovaciJmeno = HttpContext.Session.GetString("jmeno"), Hash = HttpContext.Session.GetString("heslo") });
            if (level == AuthLevel.NONE) { return RedirectToAction("AutorizaceFailed", "Home"); }
            bool isAdmin = level == AuthLevel.ADMIN;
            var ktereJmenoPouzivat = (isAdmin) ? HttpContext.Session.GetString("emulovaneJmeno") : HttpContext.Session.GetString("jmeno");
            if (isAdmin && ktereJmenoPouzivat != HttpContext.Session.GetString("jmeno")) level = AuthController.GetLevel(ktereJmenoPouzivat);
            if (level == AuthLevel.OUTER) { return RedirectToAction("AutorizaceFailed", "Home"); }*/
            //TODO tady zavolat všechny prvky
            List<Registrovany> listRegistrovanych = RegisterDBController.GetAllRegisterEntries();
            return View(listRegistrovanych);
        }

        public IActionResult PridatRegistraci([FromBody] Registrovany data)
        {
            if (!ModelState.IsValid)
            {
                //TODO tady by měl být error message, ale to nevím jak se momentálně dělá
                return Index();
            }
            RegisterDBController.CreateRegisterEntry(data.Jmeno, data.Prijmeni, data.Email, data.City, data.Street,
                                                    data.HouseNumber.ToString(), data.UserName, PasswordHelper.HashPassword(data.Password));
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public void AddRegister([FromBody] JsonElement data)
        {
            if (!data.Equals(null))
            {
                HiearchickyController.SchvaleniUctu(data.GetProperty("jmeno").GetString(), data.GetProperty("prijmeni").GetString(),
                    data.GetProperty("email").GetString(), data.GetProperty("city").GetString(), data.GetProperty("street").GetString(),
                    data.GetProperty("houseNumber").GetString(), data.GetProperty("userName").GetString(), data.GetProperty("password").GetString(),2);
            }
        }

        [HttpDelete]
        public void DeleteRegister([FromBody] JsonElement userName)
        {
            if (!userName.Equals(null))
            {
                RegisterDBController.DeleteRegisterEntry(userName.GetProperty("data").GetString());
            }
        }

    }
}
