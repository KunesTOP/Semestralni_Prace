using Microsoft.AspNetCore.Mvc;
using Models.DatabaseControllers;
using Semestralni_prace.Models.Classes;
using System.Runtime.CompilerServices;

namespace Semestralni_prace.Controllers
{
    public class KlinikyController : Controller
    {
        List<Adresy> tableNames = (List<Adresy>)AdresyController.GetAll();
        public IActionResult ZamestnanciList()
        {
            List<string> listMest = new List<string>();
            foreach(Adresy ad in tableNames)
            {
                listMest.Add(ad.City);
            }
            ViewBag.TableNames = listMest;
            return View();
        }
        //TODO přepsat návratový typ
        public IActionResult LoadTable(string tableName)
        {
            //TODO:Dopsat a zkontrolvoat tudle logiku: Vezmu si ID klinik. Podle toho si najdu adresy a budu vypisovat jen Mesta.
            Adresy spravnaAdresa = tableNames.FirstOrDefault(x => x.City == tableName);
            int? spravnaKlinika = VeterinarniKlinikaController.GetKlinikaIdByAdresa(spravnaAdresa.City,spravnaAdresa.Street,spravnaAdresa.HouseNumber);

            List<Zamestnanec> result/* = GetSpravnouKliniku(spravnaKlinika)*/ = null;

            if (result != null)
            {
                return Ok(new { data = result });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
