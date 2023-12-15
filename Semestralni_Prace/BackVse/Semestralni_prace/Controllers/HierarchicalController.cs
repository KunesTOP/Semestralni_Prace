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
        // GET: Hierarchical/Index
        public IActionResult hierarchicky()
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

            return View(listZviratekSVěkem);
        }
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
    }
}
