namespace Semestralni_prace.Models.Classes
{
    public class Zaznam
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
        public List<Zaznam> Zaznamy { get; set; }//TODO todle nějak upravit, protože to je blbost

        public Zaznam(string jmeno, string pohlavi, DateOnly narozeni, DateOnly umrti, string vlastnik, Rasa plemeno, int cerveny, int bily, string lek)
        {
            Jmeno = jmeno;
            Pohlavi = pohlavi;
            Narozeni= narozeni;
            Umrti = umrti;
            JmenoVlastnika= vlastnik;
            NazevPlemene = plemeno.JmenoRasa;
            MnozstviCervenychKrvinek = cerveny;
            MnozstviBilychKrvinek = bily;
            NazevLeku = lek;
        }   
    }
}
