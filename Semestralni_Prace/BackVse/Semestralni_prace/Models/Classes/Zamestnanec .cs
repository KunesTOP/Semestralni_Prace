using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_prace.Models.Classes
{
    public class Zamestnanec
    {
        public int IdZamestnanec { get; set; }
        public string? Jmeno { get; set; }
        public string? Prijmeni { get; set; }
        public int VeterKlinId { get; set; }
        public string? Profese { get; set; }
    }
}
