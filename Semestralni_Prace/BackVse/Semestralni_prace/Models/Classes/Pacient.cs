namespace Semestralni_prace.Models.Classes
{
    public class Pacient
    {
        public string Jmeno { get; set; }
        public string Pohlavi { get; set; }
        public DateTime Narozeni { get; set; }
        public DateTime? Umrti { get; set; }
        public string Rasa { get; set; }
        public string JmenoVlastnik { get; set; }
        public string Email { get; set; }
        public long Telefon { get; set; }
        public int CisloPrukazu { get; set; }
        public int CisloChipu { get; set; }

        public Pacient(string jmeno, string pohlavi, DateTime narozeni, DateTime? umrti,
            string rasa, string jmenoVlastnik, string email, long telefon, int cisloPrukazu,
            int cisloChipu)
        {
            Jmeno = jmeno;
            Pohlavi = pohlavi;
            Narozeni = narozeni;
            Umrti = umrti;
            Rasa = rasa;
            JmenoVlastnik = jmenoVlastnik;
            Email = email;
            Telefon = telefon;
            CisloPrukazu = cisloPrukazu;
            CisloChipu = cisloChipu;
        }


    }
}
