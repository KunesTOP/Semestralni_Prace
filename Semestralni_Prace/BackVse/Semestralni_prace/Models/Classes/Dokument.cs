using System.Security.Policy;

namespace Semestralni_prace.Models.Classes
{
    public class Dokument
    {
        public int IdDokument { get; set; }
        public string? Pripona { get; set; }
        public object? DokumentData { get; set; }
        public string DokumentNazev { get; set; }

    }
}
