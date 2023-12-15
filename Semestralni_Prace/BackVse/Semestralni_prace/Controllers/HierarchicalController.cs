using Microsoft.AspNetCore.Mvc;
using Semestralni_prace.Models.DatabaseControllers;

namespace Semestralni_prace.Controllers
{
    public class HierarchicalController : Controller
    {
        // GET: Hierarchical/Index
        public ActionResult hiearchicky()
        {
            return View();
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
