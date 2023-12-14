using ConsoleApp1.Models.DatabaseControllers;
using Microsoft.AspNetCore.Mvc;
using Semestralni_prace.Models.Classes;

namespace Semestralni_prace.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
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

                return Index();
            }
            //TODO vratit do Controlleru registrace a mít na to tabulku
            return RedirectToAction("Login","Home");
        }


    }
}
