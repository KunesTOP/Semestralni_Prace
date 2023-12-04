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
            return View(VakcinyTableController.GetAsistentPodaVakcinuZviretiView());
        }
    }
}
