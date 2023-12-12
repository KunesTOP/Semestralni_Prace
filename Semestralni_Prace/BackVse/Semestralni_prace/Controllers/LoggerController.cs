using Back.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Semestralni_prace.Models.Classes;
using Semestralni_prace.Models.DatabaseControllers;

namespace Semestralni_prace.Controllers
{
    public class LoggerController : Controller
    {
        public IActionResult Index()
        {
            var level = AuthController.Check(new AuthToken { PrihlasovaciJmeno = HttpContext.Session.GetString("jmeno"), Hash = HttpContext.Session.GetString("heslo") });
            if(level == AuthLevel.NONE) { return RedirectToAction("AutorizaceFailed", "Home"); }
            bool isAdmin = level == AuthLevel.ADMIN;
            var ktereJmenoPouzivat = (isAdmin) ? HttpContext.Session.GetString("emulovaneJmeno") : HttpContext.Session.GetString("jmeno");
            if (isAdmin && ktereJmenoPouzivat != HttpContext.Session.GetString("jmeno")) level = AuthController.GetLevel(ktereJmenoPouzivat);
            List<Logger> listLoggeru = LoggerDBController.GetAll();

            return View(listLoggeru);
        }
    }
}
