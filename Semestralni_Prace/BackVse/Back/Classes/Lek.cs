﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class Lek
    {
        

        public int Id { get; set; }
        
        public DateTime Prescribed { get; set; }
        public string? Nazev { get; internal set; }
    }
}