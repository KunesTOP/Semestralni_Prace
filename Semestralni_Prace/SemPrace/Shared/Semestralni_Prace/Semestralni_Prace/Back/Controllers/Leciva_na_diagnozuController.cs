using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class LecivaNaDiagnozuController
    {
        public const string TABLE_NAME = "LECIVA_NA_DIAGNOZU";
        public const string LEKY_ID_NAME = "leky_id_leky";
        public const string ANAMN_ID_NAME = "anamn_id_anamn";

        public static IEnumerable<int> GetLekyIds(int anamnId)
        {
            return GetIds(TABLE_NAME, LEKY_ID_NAME, ANAMN_ID_NAME, anamnId);
        }

        public static IEnumerable<int> GetAnamnIds(int lekyId)
        {
            return GetIds(TABLE_NAME, ANAMN_ID_NAME, LEKY_ID_NAME, lekyId);
        }

        public static void InsertMapping(int lekyId, int anamnId)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({LEKY_ID_NAME}, {ANAMN_ID_NAME}) VALUES (:lekyId, :anamnId)",
                new OracleParameter("lekyId", lekyId),
                new OracleParameter("anamnId", anamnId)
            );
        }

        // Další metody podle potřeby...

        // Tato metoda získá seznam ID podle zadaných podmínek
        private static IEnumerable<int> GetIds(string tableName, string idColumnName, string conditionColumnName, int conditionValue)
        {
            List<int> ids = new List<int>();

            DataTable query = DatabaseController.Query($"SELECT {idColumnName} FROM {tableName} WHERE {conditionColumnName} = :conditionValue",
                new OracleParameter("conditionValue", conditionValue));

            foreach (DataRow dr in query.Rows)
            {
                ids.Add(int.Parse(dr[idColumnName].ToString()));
            }

            return ids;
        }
    }
}
