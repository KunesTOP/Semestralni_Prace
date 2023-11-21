using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class VakcinaPodavanaZviretiController : Controller
    {
        public const string TABLE_NAME = "VAKCINA_PODAVANA_ZVIRETI";
        public const string VAKCINA_ID_NAME = "vakcina_id_vakcina";
        public const string ZVIRE_ID_NAME = "zvire_id_zvire";

        public static IEnumerable<int> GetZvireIds(int vakcinaId)
        {
            return GetIds(TABLE_NAME, ZVIRE_ID_NAME, VAKCINA_ID_NAME, vakcinaId);
        }

        public static IEnumerable<int> GetVakcinaIds(int zvireId)
        {
            return GetIds(TABLE_NAME, VAKCINA_ID_NAME, ZVIRE_ID_NAME, zvireId);
        }

        public static void InsertMapping(int vakcinaId, int zvireId)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({VAKCINA_ID_NAME}, {ZVIRE_ID_NAME}) VALUES (:vakcinaId, :zvireId)",
                new OracleParameter("vakcinaId", vakcinaId),
                new OracleParameter("zvireId", zvireId)
            );
        }

      
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
