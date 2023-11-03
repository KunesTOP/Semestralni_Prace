using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class MajitelVlastniPrukazController
    {
        public const string TABLE_NAME = "MAJITEL_VLASTNI_PRUKAZ";
        public const string MAJITEL_ID_NAME = "majitel_zvire_id_pacient";
        public const string PRUKAZ_ID_NAME = "prukaz_id_prukaz";

        public static IEnumerable<int> GetMajitelIds(int prukazId)
        {
            return GetIds(TABLE_NAME, MAJITEL_ID_NAME, PRUKAZ_ID_NAME, prukazId);
        }

        public static IEnumerable<int> GetPrukazIds(int majitelId)
        {
            return GetIds(TABLE_NAME, PRUKAZ_ID_NAME, MAJITEL_ID_NAME, majitelId);
        }

        public static void InsertMapping(int majitelId, int prukazId)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({MAJITEL_ID_NAME}, {PRUKAZ_ID_NAME}) VALUES (:majitelId, :prukazId)",
                new OracleParameter("majitelId", majitelId),
                new OracleParameter("prukazId", prukazId)
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
