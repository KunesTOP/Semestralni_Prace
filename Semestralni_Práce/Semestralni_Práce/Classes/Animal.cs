using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name {get; set; }
        public Sex Sex { get; set; }
        public DateTime Birth { get; set; }
        public DateTime? Death { get; set; }
        public int? Owner { get; set; }
        public int? IdentificationCardId { get; set; }
        public int? BreedId { get; set; }

        //TODO domyslet jak bude vyřešená vakcína a Nemoc, jestli Animal bude mít id na vakcínu a nemoc,
        //nebo je napojit na jinou třídu
    }
}
