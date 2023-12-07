using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Data;

namespace Semestralni_prace.Models.DatabaseControllers
{
    public class DokumentController
    {
        private const string TABLE_NAME = "DOKUMENTY";
        private const string ID_DOKUMENTY_NAME = "ID_DOKUMENT";
        private const string PRIPOMA_DOKUMENTY_NAME = "PRIPONA";
        private const string DATA_DOKUMENT_NAME = "DOKUMENT";
        private const string DOKUMENT_NAME = "NAZEV_SOUBORU";

        //TODO: Zkontrolovat, jestli todle je správně
        public static Dokument Get(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_DOKUMENTY_NAME} = :id",
                new OracleParameter("id", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Dokument()
            {
                IdDokument = int.Parse(query.Rows[0][ID_DOKUMENTY_NAME].ToString()),
                Pripona = query.Rows[0][PRIPOMA_DOKUMENTY_NAME].ToString(),
                DokumentData = query.Rows[0][DATA_DOKUMENT_NAME],
                DokumentNazev = query.Rows[0][DOKUMENT_NAME].ToString()
            };
        }
        public static IEnumerable<Dokument> GetAll()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");

            if (query.Rows.Count == 0)
            {
                return null;
            }
            List<Dokument> listDokumentu = new List<Dokument>();
            foreach (DataRow row in query.Rows)
            {
                listDokumentu.Add(new Dokument()
                {
                    IdDokument = int.Parse(query.Rows[0][ID_DOKUMENTY_NAME].ToString()),
                    Pripona = query.Rows[0][PRIPOMA_DOKUMENTY_NAME].ToString(),
                    DokumentData = query.Rows[0][DATA_DOKUMENT_NAME],
                    DokumentNazev = query.Rows[0][DOKUMENT_NAME].ToString()
                });
            }
            return listDokumentu;
        }
    }
}
