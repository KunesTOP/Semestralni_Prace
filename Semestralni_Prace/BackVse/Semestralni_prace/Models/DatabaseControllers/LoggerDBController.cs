using Back.databaze;
using Semestralni_prace.Models.Classes;
using System.Data;

namespace Semestralni_prace.Models.DatabaseControllers
{
    public class LoggerDBController
    {
        public const string TABLE_NAME = "LOGTABLE";
        public const string TABULKA_ZPRAVA_NAME = "TABULKA";
        public const string UDALOST_NAME = "UDALOST";
        public const string CAS_NAME = "CAS";
        public const string ZPRAVA_NAME = "ZPRAVA";

        public static List<Logger> GetAll()
        {
            List<Logger> listLogger = new List<Logger>();
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");

            if (query.Rows.Count == 0)
            {
                return null;
            }

            foreach (DataRow row in query.Rows)
            {
                listLogger.Add(new Logger
                {
                    TabulkaNazev = row[TABULKA_ZPRAVA_NAME].ToString(),
                    Udalost = row[UDALOST_NAME].ToString(),
                    Cas = DateTime.Parse(row[CAS_NAME].ToString()),
                    Zprava = row[ZPRAVA_NAME].ToString()
                });
            };
            return listLogger;
        }


    }
}
