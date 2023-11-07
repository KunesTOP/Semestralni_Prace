using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class Majitel
    {
     
        public int PacientId { get; internal set; }
        public string? Mail { get; internal set; }
        public string? Telefon { get; internal set; }
        public string? Jmeno { get; internal set; }
        public string? Prijmeni { get; internal set; }
        public int VetKlinId { get; internal set; }
        public int IdMajitel { get; internal set; }
    }
}
