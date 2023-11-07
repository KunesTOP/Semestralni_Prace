using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using Semestralni_Práce.Classes;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class AsistentiController//TODO knihovna dole 
    {
        public const string TABLE_NAME = "ASISTENTI";
        public const string ID_NAME = "id_zamestnanec";
        public const string PRAXE_NAME = "praxe";

        public static IEnumerable<int> GetIds()
        {
            return GetIds(TABLE_NAME, ID_NAME);
        }

        public static Asistent Get(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Asistent()
            {
                Id = int.Parse(query.Rows[0][ID_NAME].ToString()),
                Praxe = int.Parse(query.Rows[0][PRAXE_NAME].ToString())
            };
        }

        public static void InsertAsistent(Asistent asistent)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({ID_NAME}, {PRAXE_NAME}) VALUES (:id, :praxe)",
                new OracleParameter("id", asistent.Id),
                new OracleParameter("praxe", asistent.Praxe)
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


