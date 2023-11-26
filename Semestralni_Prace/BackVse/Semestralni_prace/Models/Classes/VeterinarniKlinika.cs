using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_prace.Models.Classes
{
    public class VeterinarniKlinika : Zamestnanec
    {
        //TODO: Upřímně nevím co s tímdle atributem... mít to jako bool a všude mít true je zbytečný... :/ takže by to chtělo 
        //trochu upravit ten model asi... přecejen jsem to psal před 3 roky
        public int Accreditation { get; set; }
        public string? JmenoMajitel { get; internal set; }
        public string? PrijmeniMajitel { get; internal set; }
        public int? VeterKlinId { get; internal set; }
        public int AdresyIdAdresa { get; internal set; }
    }
}
