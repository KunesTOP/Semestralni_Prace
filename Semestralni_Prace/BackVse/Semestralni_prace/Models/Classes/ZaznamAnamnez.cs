namespace Semestralni_prace.Models.Classes
{
    public class ZaznamAnamnez
    {
        public string Jmeno { get; set; }
        public string Pohlavi { get; set; }
        public DateOnly Narozeni { get; set; }
        public DateOnly? Umrti { get; set; }
        public string JmenoVlastnika { get; set; }
        public string NazevPlemene { get; set; }
        public int MnozstviBilychKrvinek { get; set; }
        public int MnozstviCervenychKrvinek { get; set; }
        public string NazevLeku { get; set; }
    }
}
