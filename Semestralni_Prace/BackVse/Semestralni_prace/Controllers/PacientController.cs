using Microsoft.AspNetCore.Mvc;
using Semestralni_prace.Models.Classes;
using Semestralni_prace.Models;
using Models.DatabaseControllers;

namespace Semestralni_prace.Controllers
{
    public class PacientController : Controller
    {

        public IActionResult ListPacientu()
        {
            List<Pacient> PacientList = GetListPacientu();
            return View(PacientList);
        }
        private List<Pacient> GetListPacientu()
        {
            List<Pacient> PacientList = new List<Pacient>();
            List<Zvire> zvireList = ZvirataController.GetAll();
            List<Majitel> majiteleList = MajiteleZviratController.GetAll();
            List<Prukaz> prukazyList = PrukazyController.GetAll();

            foreach (var prukaz in prukazyList)
            {
                try
                {
                    Zvire zvire = zvireList.FirstOrDefault(item => item.IdZvire == prukaz.ZvireId);
                    Majitel majitel = majiteleList.FirstOrDefault(item => item.PacientId == zvire.MajitelZvireIdPacient);
                    PacientList.Add(new Pacient
                    {

                        Jmeno = zvire.JmenoZvire,
                        Pohlavi = zvire.Pohlavi,
                        Narozeni = zvire.DatumNarozeni,
                        Umrti = zvire.DatumUmrti,
                        Rasa = RasaZviratController.GetById(zvire.RasaZviratIdRasa).JmenoRasa,
                        JmenoVlastnik = majitel.CeleJmeno(),
                        Email = majitel.Mail,
                        Telefon = Int64.Parse(majitel.Telefon),
                        CisloPrukazu = prukaz.CisloPrukaz,
                        CisloChipu = prukaz.CisloChip
                    });
                }
                catch (Exception ex)
                {
                   
                }
            }
            return PacientList;
        }
        public IActionResult PacientAdd()
        {
            return View();
        }
    }
}
