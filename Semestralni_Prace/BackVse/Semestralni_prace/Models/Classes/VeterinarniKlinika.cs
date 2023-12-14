using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_prace.Models.Classes
{
    public class VeterinarniKlinika
    {
        public string? JmenoMajitel { get; set; }
        public string? PrijmeniMajitel { get; set; }
        public int Id { get; set; }
        public int AdresyIdAdresa { get; set; }
    }
}
