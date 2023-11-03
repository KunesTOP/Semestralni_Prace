using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class Owner
    {
        public int Id { get; set; }
        public string Forename { get; set; }
        public string Lastname { get; set; }
        public long? Phonenumber { get; set; }
        public string EmailAdress { get; set; }
        //Zvířata pod jeho jménem, jen mě zkontroluj, jestli to dává smysl
        public List<Animal> Animals { get; set; }
    }
}
