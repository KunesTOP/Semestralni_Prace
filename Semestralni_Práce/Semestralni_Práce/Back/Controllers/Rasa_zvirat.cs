using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class RasaZviratController//TODO knihovna
    {
        public const string TABLE_NAME = "RASA_ZVIRAT";
        public const string JMENO_RASA_NAME = "jmeno_rasa";
        public const string ID_RASA_NAME = "id_rasa";

        public static IEnumerable<int> GetRasaIds()
        {
            return GetIds(TABLE_NAME, ID_RASA_NAME);
        }

        public static Rasa GetByJmenoRasa(string jmenoRasa)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {JMENO_RASA_NAME} = :jmenoRasa",
                new OracleParameter("jmenoRasa", jmenoRasa));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Rasa()
            {
                JmenoRasa = query.Rows[0][JMENO_RASA_NAME].ToString(),
                IdRasa = int.Parse(query.Rows[0][ID_RASA_NAME].ToString())
            };
        }

        public static void InsertRasa(Rasa rasa)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({JMENO_RASA_NAME}, {ID_RASA_NAME}) " +
                $"VALUES (:jmenoRasa, :idRasa)",
                new OracleParameter("jmenoRasa", rasa.JmenoRasa),
                new OracleParameter("idRasa", rasa.IdRasa)
            );
        }

        // Další metody podle potřeby...

        // Tato metoda získá seznam ID podle zadaných podmínek
        private static IEnumerable<int> GetIds(string tableName, string idColumnName)
        {
            List<int> ids = new List<int>();

            DataTable query = DatabaseController.Query($"SELECT {idColumnName} FROM {tableName}");

            foreach (DataRow dr in query.Rows)
            {
                ids.Add(int.Parse(dr[idColumnName].ToString()));
            }

            return ids;
        }
    }
}


