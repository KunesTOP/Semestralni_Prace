using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_prace.Models.Classes
{
    public class Majitel
    {
        public int Id { get; set; }
        public string? Mail { get; set; }
        public string? Telefon { get; set; }
        public string? Jmeno { get; set; }
        public string? Prijmeni { get; set; }
        public int VetKlinId { get; set; }
        public int IdMajitel { get; set; }


        public string CeleJmeno()
        {
            return Jmeno + " " + Prijmeni;
        }
    }
}
