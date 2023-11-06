using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class Zamestnanec
    {
        //TODO: Dohodnout se na tom Id tady
        public int Id { get; set; }
        public int? TitleId { get; set; }
        public int IdZamestnanec { get; internal set; }
        public string? Jmeno { get; internal set; }
        public string? Prijmeni { get; internal set; }
        public int VeterKlinId { get; internal set; }
        public string? Profese { get; internal set; }
    }
}
