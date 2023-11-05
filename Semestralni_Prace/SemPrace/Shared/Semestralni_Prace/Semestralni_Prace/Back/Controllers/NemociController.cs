using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class NemociController
    {
        public const string TABLE_NAME = "NEMOCI";
        public const string NEMOC_ID_NAME = "nemoc_id";

        public static IEnumerable<int> GetNemocIds()
        {
            return GetIds(TABLE_NAME, NEMOC_ID_NAME);
        }

        public static void InsertNemoc(int nemocId)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({NEMOC_ID_NAME}) VALUES (:nemocId)",
                new OracleParameter("nemocId", nemocId)
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
