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
            List<Zvire> listZvirat = ZvirataController.GetAll();
            List<Asistent> listAsistentu = AsistentiController.GetAll();
            List<Vakcina> listVakcin = VakcinyTableController.GetAll();





            return View();
        }
    }
}
