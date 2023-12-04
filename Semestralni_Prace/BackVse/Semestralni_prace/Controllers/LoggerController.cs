using Microsoft.AspNetCore.Mvc;
using Semestralni_prace.Models.Classes;
using Semestralni_prace.Models.DatabaseControllers;

namespace Semestralni_prace.Controllers
{
    public class LoggerController : Controller
    {
        public IActionResult Index()
        {
            List<Logger> listLoggeru = LoggerDBController.GetAll();

            return View(listLoggeru);
        }
    }
}
