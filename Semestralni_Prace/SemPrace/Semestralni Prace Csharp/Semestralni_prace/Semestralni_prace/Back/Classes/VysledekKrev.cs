﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class VysledekKrev
    {
        public int Id { get; set; }
        public int AmountOfAntibodies { get; set; }
        public int AmountOfRedBloodCells { get; set; }
        public int MnozstviProtilatky { get; internal set; }
        public int IdVysledek { get; internal set; }
        public int MnozstviCervKrv { get; internal set; }
        public int? AnamnezaId { get; internal set; }
    }
}
