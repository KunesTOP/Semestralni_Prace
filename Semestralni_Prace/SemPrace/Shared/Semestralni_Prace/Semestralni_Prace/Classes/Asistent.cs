using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class Asistent:Zamestnanec
    {
        //TODO: Praxe počítat na hodiny jako... čistě int nebo to přidat datum nástupu a odečítat od aktuálního času?
        //To je lepší...
        public int AmountOfExperience { get; set; }
        public int Praxe { get; internal set; }
        //TODO Proč by potřeboval Asistent Id, když ho dědí ze zaměstnance?
        //public int Id { get; internal set; }
    }
}
