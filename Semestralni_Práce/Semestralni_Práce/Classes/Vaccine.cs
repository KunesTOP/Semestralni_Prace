using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class Vaccine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //TODO: tady mám asi chybu v modelu, že je prodává Asistent... takže to bych s tebou probral ještě.
        //Jestli přidáme datum podání vakcíny a potom sem přidáme AsistentId, který jí administroval
        //nebo nevím jak jinak to vyřešit

    }
}
