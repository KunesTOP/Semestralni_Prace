using Oracle.ManagedDataAccess.Client;

namespace Semestralni_prace.Models.Classes
{
    public class ZaznamAnamnez
    {
        public int VysledekKrveId { get; set; }
        public int MnozstviBilychKrvinek { get; set; }
        public int MnozstviCervenychKrvinek { get; set; }
        public DateTime Datum { get; set; }
        public int AnamnezaId { get; set; }
    }
}
