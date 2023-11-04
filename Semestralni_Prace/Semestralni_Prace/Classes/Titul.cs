﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class Titul
    {
        public int Id { get; set; }
        public int Abbreviation { get; set; }
        public int Name { get; set; }
        public int IdTitul { get; internal set; }
        public string? ZkratkaTitul { get; internal set; }
        public string? NazevTitul { get; internal set; }
    }
}
