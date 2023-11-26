using Microsoft.AspNetCore.Mvc;
using Semestralni_prace.Models.Classes;
using Semestralni_prace.Models;
using Models.DatabaseControllers;

namespace Semestralni_prace.Controllers
{
    public class PacientController : Controller
    {
        public List<Pacient> PacientList = new List<Pacient>();

        public IActionResult ListPacientu()
        {
            List<Zvire> zvireList = ZvirataController.GetAll();
            List<Majitel> majiteleList = MajiteleZviratController.GetAll();
            List<Prukaz> prukazyList = PrukazyController.GetAll();

            foreach (var prukaz in prukazyList)
            {
                try
                {
                    Zvire zvire = zvireList.FirstOrDefault(item => item.IdZvire == prukaz.ZvireId);
                    Majitel majitel = majiteleList.FirstOrDefault(item => item.PacientId == zvire.MajitelZvireIdPacient);
                    PacientList.Add(new Pacient(
                        zvire.JmenoZvire, zvire.Pohlavi, zvire.DatumNarozeni, zvire.DatumUmrti, RasaZviratController.GetById(zvire.RasaZviratIdRasa).JmenoRasa,
                        majitel.CeleJmeno(), majitel.Mail, Int64.Parse(majitel.Telefon), prukaz.CisloPrukaz, prukaz.CisloChip));
             }
             catch (Exception ex)
             {                    
             }
         }
            return View(PacientList);
        }
    }
}
