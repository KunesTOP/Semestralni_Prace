﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class Adress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int? HouseNumber { get; set; }
        public string City { get; set; }
        public int? PostalCode { get; set; }

    }
}
