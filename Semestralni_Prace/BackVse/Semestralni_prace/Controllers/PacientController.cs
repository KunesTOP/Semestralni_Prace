using Microsoft.AspNetCore.Mvc;
using Semestralni_prace.Models.Classes;
using Semestralni_prace.Models;
using Models.DatabaseControllers;
using Back.Auth;
using System.Text.Json;

namespace Semestralni_prace.Controllers
{
    public class PacientController : Controller
    {
        public IActionResult ListPacientu()
        {
            var level = AuthController.Check(new AuthToken { PrihlasovaciJmeno = HttpContext.Session.GetString("jmeno"),Hash = HttpContext.Session.GetString("heslo") });
            if (level == AuthLevel.NONE) { return RedirectToAction("AutorizaceFailed", "Home"); }
            bool isAdmin = level == AuthLevel.ADMIN;
            var ktereJmenoPouzivat = (isAdmin) ? HttpContext.Session.GetString("emulovaneJmeno") : HttpContext.Session.GetString("jmeno");
            if (isAdmin && ktereJmenoPouzivat != HttpContext.Session.GetString("jmeno")) level = AuthController.GetLevel(ktereJmenoPouzivat);
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
                    Zvire? zvire = zvireList.FirstOrDefault(item => item.Id == prukaz.ZvireId);
                    Majitel? majitel = majiteleList.FirstOrDefault(item => item.Id == zvire.MajitelZvireIdPacient);
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
            List<Rasa> listRas = (List<Rasa>)RasaZviratController.GetAll();
            List<Adresy> listAdres = (List<Adresy>)AdresyController.GetAll();

            var tuple = new Tuple<List<Rasa>, List<Adresy>>(listRas, listAdres);

            return View(tuple);
        }

        [HttpPost]
        public IActionResult BtnAdd([FromBody] JsonElement data)
        {
            int idKliniky = AdresyController.GetIdByCity(data.GetProperty("klinikaMesto").GetString());
            int majitelId = MajiteleZviratController.UpsertMajitelPacient(idKliniky,data);
            int zvireId = ZvirataController.UpsertZvirePacient(majitelId,data);
            Prukaz prukaz = new Prukaz { Id = -1,
                ZvireId = zvireId,
                CisloChip = int.Parse(data.GetProperty("cisloChip").GetString()),
                CisloPrukaz = int.Parse(data.GetProperty("cisloPrukaz").GetString())
            };
            PrukazyController.InsertPrukaz(prukaz);
            return RedirectToAction("Pacient", "ListPacientu");
        }
    }
}
