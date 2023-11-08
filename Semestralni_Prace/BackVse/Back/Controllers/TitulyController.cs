using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using Semestralni_Práce.Classes;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class TitulyController//TODO KNIHOVNA
    {
        public const string TABLE_NAME = "TITULY";
        public const string ID_TITUL_NAME = "id_titul";
        public const string ZKRATKA_TITUL_NAME = "zkratka_titul";
        public const string NAZEV_TITUL_NAME = "nazev_titul";

        public static IEnumerable<int> GetTitulIds()
        {
            return GetIds(TABLE_NAME, ID_TITUL_NAME);
        }

        public static Titul GetByZkratkaTitul(string zkratkaTitul)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ZKRATKA_TITUL_NAME} = :zkratkaTitul",
                new OracleParameter("zkratkaTitul", zkratkaTitul));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Titul()
            {
                IdTitul = int.Parse(query.Rows[0][ID_TITUL_NAME].ToString()),
                ZkratkaTitul = query.Rows[0][ZKRATKA_TITUL_NAME].ToString(),
                NazevTitul = query.Rows[0][NAZEV_TITUL_NAME].ToString()
            };
        }
        public static Titul GetByTitulId(int idTitul)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_TITUL_NAME} = :idTitul",
                new OracleParameter("idTitul", idTitul));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Titul()
            {
                IdTitul = int.Parse(query.Rows[0][ID_TITUL_NAME].ToString() ),
                ZkratkaTitul = query.Rows[0][ZKRATKA_TITUL_NAME].ToString(),
                NazevTitul = query.Rows[0][NAZEV_TITUL_NAME].ToString()
            };
        }

        public static void InsertTitul(Titul titul)
        {Titul existingTitul = GetByTitulId(titul.IdTitul);

            if (existingTitul == null)
            {

                DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({ID_TITUL_NAME}, {NAZEV_TITUL_NAME}, {ZKRATKA_TITUL_NAME}) " +
                    $"VALUES (:idTitul, :nazevTitul, :zkratkaTitul)",
                    new OracleParameter("idTitul", titul.IdTitul),
                    new OracleParameter("nazevTitul", titul.NazevTitul),
                    new OracleParameter("zkratkaTitul", titul.ZkratkaTitul)
                );
            }
            else
               
            {
                RemoveTitul(titul.IdTitul);
                DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({ID_TITUL_NAME}, {NAZEV_TITUL_NAME}, {ZKRATKA_TITUL_NAME}) " +
            $"VALUES (:idTitul, :nazevTitul, :zkratkaTitul)",
            new OracleParameter("idTitul", titul.IdTitul ),
            new OracleParameter("nazevTitul", titul.NazevTitul),
            new OracleParameter("zkratkaTitul", titul.ZkratkaTitul));
            }
        }

        public static void RemoveTitul(int idTitul)
        {
            DatabaseController.Execute($"DELETE FROM {TABLE_NAME} WHERE {ID_TITUL_NAME} = :idTitul",
                new OracleParameter("idTitul", idTitul)
            );
        }
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


