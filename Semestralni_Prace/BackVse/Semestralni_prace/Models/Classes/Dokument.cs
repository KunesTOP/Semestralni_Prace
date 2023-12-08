using System.Security.Policy;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Semestralni_prace.Models.Classes
{
    public class Dokument
    {
        public int IdDokument { get; set; }
        public string? Pripona { get; set; }
        public string? Data { get; set; }
        public string DokumentNazev { get; set; }

        public void LoadBytes(byte[] bytes)
        {
            Data = Dokument.SerializeBytes(bytes);
        }

        public byte[] GetBytes()
        {
            return Dokument.DeserializeBytes(Data);
        }

        public static string SerializeBytes(byte[] bytes)
        {
            return System.Convert.ToBase64String(bytes);
        }

        public static byte[] DeserializeBytes(string source)
        {
            return System.Convert.FromBase64String(source);
        }

    }
}
