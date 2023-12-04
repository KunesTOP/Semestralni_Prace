using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_prace.Models.Classes
{
    public class VeterinarniKlinika : Zamestnanec
    {
        public string? JmenoMajitel { get; internal set; }
        public string? PrijmeniMajitel { get; internal set; }
        public int? VeterKlinId { get; internal set; }
        public int AdresyIdAdresa { get; internal set; }
    }
}
