using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class Clinic
    {
        public int OwnerId { get; set; }
        public string Forename { get; set; }
        public string Lastname { get; set; }  
        public int? AdressId { get; set; }

        /*TODO: Má tady být seznam zaměstnanců a pacientu? tedy
         * public List<Employee> Employees { get; set; }
         * public List<Owner> AnimalOwners {get; set;}
         */
    }
}
