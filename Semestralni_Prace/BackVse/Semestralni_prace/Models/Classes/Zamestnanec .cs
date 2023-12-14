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
        public string? Jmeno { get; set; }
        public string? Prijmeni { get; set; }
        public int VeterKlinId { get; set; }
        public string? Profese { get; set; }
        public override string ToString()
        {
            return $"IdZamestnanec: {Id}, Jmeno: {Jmeno}, Prijmeni: {Prijmeni}, VeterKlinId: {VeterKlinId}, Profese: {Profese}";
        }
    }
}
