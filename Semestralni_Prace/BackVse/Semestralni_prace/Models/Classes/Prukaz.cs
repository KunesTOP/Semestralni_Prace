using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_prace.Models.Classes
{
    public class Prukaz
    {
        public int CisloPrukaz { get; internal set; }
        public int CisloChip { get; internal set; }
        public int Id { get; internal set; }
        public int? ZvireId { get; internal set; }
    }
}
