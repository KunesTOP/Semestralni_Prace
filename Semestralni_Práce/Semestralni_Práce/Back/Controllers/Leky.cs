using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class LekyController//TODO knihovna
    {
        public const string TABLE_NAME = "LEKY";
        public const string ID_NAME = "id_leky";
        public const string NAZEV_NAME = "nazev";

        public static IEnumerable<int> GetIds()
        {
            return GetIds(TABLE_NAME, ID_NAME);
        }

        public static Lek Get(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Lek()
            {
                Id = int.Parse(query.Rows[0][ID_NAME].ToString()),
                Nazev = query.Rows[0][NAZEV_NAME].ToString()
            };
        }

        public static void InsertLek(Lek lek)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({ID_NAME}, {NAZEV_NAME}) VALUES (:id, :nazev)",
                new OracleParameter("id", lek.Id),
                new OracleParameter("nazev", lek.Nazev)
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


