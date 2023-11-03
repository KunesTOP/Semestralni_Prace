using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    internal class Veterinarian:Employee
    {
        //TODO: Upřímně nevím co s tímdle atributem... mít to jako bool a všude mít true je zbytečný... :/ takže by to chtělo 
        //trochu upravit ten model asi... přecejen jsem to psal před 3 roky
        public int Accreditation { get; set; }
    }
}
