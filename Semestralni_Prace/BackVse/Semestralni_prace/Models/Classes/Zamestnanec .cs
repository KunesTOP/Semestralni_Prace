using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_prace.Models.Classes
{
    public class Zamestnanec
    {
        public int Id { get; set; }
        public string Forename { get; set; }
        public string Lastname { get; set; }
        public int? TitleId { get; set; }
        public int IdZamestnanec { get; internal set; }
        public string? Jmeno { get; internal set; }
        public string? Prijmeni { get; internal set; }
        public int VeterKlinId { get; internal set; }
        public string? Profese { get; internal set; }
    }
}
