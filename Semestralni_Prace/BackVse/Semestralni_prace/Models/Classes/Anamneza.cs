using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestralni_prace.Models.Classes
{
    public class Anamneza
    {
        public int Id { get; set; }
        public DateTime Datum { get; internal set; }
    }
}
