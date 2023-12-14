using Microsoft.AspNetCore.Mvc;

namespace Semestralni_prace.Controllers
{
    public class KlinikyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //TODO přepsat návratový typ
        public void LoadTable(string tableName)
        {
            //TODO:Dopsat a zkontrolvoat tudle logiku: naplnění selectu městy adres a podle toho vybrání pobočky 
          /*  List<object> result = GetSpravnouKliniku(tableName);

            if (result != null)
            {
                return Ok(new { data = result });
            }
            else
            {
                return NotFound();
            }*/
        }
        //TODO: Dopsat metodu GetSpravnouKliniku pro určení, která klinika je vybraná a který zaměstnance vybrat
    }
}
