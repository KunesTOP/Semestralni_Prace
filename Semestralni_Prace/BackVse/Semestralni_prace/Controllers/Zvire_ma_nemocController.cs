using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class ZvireMaNemocController : Controller
    {
        public const string TABLE_NAME = "ZVIRE_MA_NEMOC";
        public const string NEMOC_NEMOC_ID_NAME = "nemoc_nemoc_id";
        public const string ZVIRE_ID_ZVIRE_NAME = "zvire_id_zvire";

        public static IEnumerable<int> GetNemocIds(int zvireId)
        {
            return GetIds(TABLE_NAME, NEMOC_NEMOC_ID_NAME, ZVIRE_ID_ZVIRE_NAME, zvireId);
        }

        public static IEnumerable<int> GetZvireIds(int nemocId)
        {
            return GetIds(TABLE_NAME, ZVIRE_ID_ZVIRE_NAME, NEMOC_NEMOC_ID_NAME, nemocId);
        }

        public static void InsertMapping(int nemocId, int zvireId)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({NEMOC_NEMOC_ID_NAME}, {ZVIRE_ID_ZVIRE_NAME}) VALUES (:nemocId, :zvireId)",
                new OracleParameter("nemocId", nemocId),
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
