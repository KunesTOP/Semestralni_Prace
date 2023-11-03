using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class Prukaz
    {
        public int Id { get; set; }
        public int NumberOfCard { get; set; }
        public int NumberOfChip { get; set; }
        public int CisloPrukaz { get; internal set; }
        public int CisloChip { get; internal set; }
        public int IdPrukaz { get; internal set; }
        public int? ZvireId { get; internal set; }
    }
}
