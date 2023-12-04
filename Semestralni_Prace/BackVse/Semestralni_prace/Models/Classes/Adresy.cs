using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_prace.Models.Classes
{
    public class Adresy
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int? HouseNumber { get; set; }
        public string City { get; set; }

    }
}
