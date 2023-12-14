namespace Semestralni_prace.Models.Classes
{
    public class Ucty
    {
        public string Jmeno { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public int Uroven { get; set; }
        public int Id { get; internal set; }
    }
}
