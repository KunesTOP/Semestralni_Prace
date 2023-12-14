using Ninject.Selection;
using Semestralni_prace.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_prace.Models.Classes
{
    public class Zvire
    {
        public string? JmenoZvire { get; internal set; }
        public string? Pohlavi { get; internal set; }
        public DateTime DatumNarozeni { get; internal set; }
        public DateTime? DatumUmrti { get; internal set; }
        public int MajitelZvireIdPacient { get; internal set; }
        public int Id { get; internal set; }
        public int RasaZviratIdRasa { get; internal set; }
    }
}