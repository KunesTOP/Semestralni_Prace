﻿using Microsoft.AspNetCore.Mvc;
using Semestralni_prace.Models.Classes;

namespace Semestralni_prace.Controllers
{
    public class ZaznamyController : Controller
    {
        public IActionResult Zaznamy()
        {
            return View();
        }
        public IActionResult ZaznamyPridat()
        {
            return View();
        }
    }
}
