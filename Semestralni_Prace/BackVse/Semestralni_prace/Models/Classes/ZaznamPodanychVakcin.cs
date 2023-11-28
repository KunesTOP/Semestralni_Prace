using System;

namespace Semestralni_prace.Models.Classes
{
    public class ZaznamPodanychVakcin
    {
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string Profese { get; set; }
        public string NazevVakcina { get; set; }
        public string? JmenoZvire { get; internal set; }
        public string? Pohlavi { get; internal set; }
        public DateTime DatumNarozeni { get; internal set; }
        public DateTime? DatumUmrti { get; internal set; }

        public ZaznamPodanychVakcin(string jmeno, string prijmeni, string profese, string nazevVakcina, string jmenoZvire, string pohlavi, DateTime datumNarozeni, DateTime datumUmrti)
        {
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Profese = profese;
            NazevVakcina = nazevVakcina;
            JmenoZvire = jmenoZvire;
            Pohlavi = pohlavi;
            DatumNarozeni = datumNarozeni;
            DatumUmrti = datumUmrti;
        }
        public string CeleJmeno()
        {
            return Jmeno  + " " + Prijmeni;
        }
    }
}