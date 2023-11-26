using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_prace.Models.Classes
{
    public class VysledekKrev
    {
        public int MnozstviProtilatky { get; internal set; }
        public int IdVysledek { get; internal set; }
        public int MnozstviCervKrv { get; internal set; }
        public int? AnamnezaId { get; internal set; }
    }
}
