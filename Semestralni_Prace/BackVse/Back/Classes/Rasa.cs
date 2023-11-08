﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class Rasa
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JmenoRasa { get; internal set; }
        public int IdRasa { get; internal set; }
    }
}