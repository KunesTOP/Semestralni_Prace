using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_Práce.Classes
{
    public class Anamnesis
    {
        public int Id { get; set; }
        public DateTime Date_of_Anamnesis { get; set; }
        public int? ResultsId { get; set; }
        public int? MedicationId { get; set; }
        public int? VeterinarianId { get; set; }

    }
}
