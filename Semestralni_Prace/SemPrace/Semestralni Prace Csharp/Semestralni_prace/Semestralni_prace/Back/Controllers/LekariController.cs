using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class LekariController//TODO dodelat knihovnu
    {
        public const string TABLE_NAME = "LEKARI";
        public const string ID_NAME = "id_zamestnanec";
        public const string AKREDITACE_NAME = "akreditace";

        public static IEnumerable<int> GetIds()
        {
            return GetIds(TABLE_NAME, ID_NAME);
        }

        //public static Lekar Get(int id)
        //{
        //    DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
        //        new OracleParameter("id", id));

        //    if (query.Rows.Count != 1)
        //    {
        //        return null;
        //    }

        //    return new Lekar()
        //    {
        //        Id = int.Parse(query.Rows[0][ID_NAME].ToString()),
        //        Akreditace = query.Rows[0][AKREDITACE_NAME].ToString()
        //    };
        //}

        //public static void InsertLekar(Lekar lekar)
        //{
        //    DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({ID_NAME}, {AKREDITACE_NAME}) VALUES (:id, :akreditace)",
        //        new OracleParameter("id", lekar.Id),
        //        new OracleParameter("akreditace", lekar.Akreditace)
        //    );
        //}

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


