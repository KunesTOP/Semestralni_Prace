using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class ZamestnanciMajiTitulyController : Controller
    {
        public const string TABLE_NAME = "ZAMESTNANCI_MAJI_TITULY";
        public const string ZAMES_ID_ZAMES_NAME = "zames_id_zames";
        public const string TITUL_ID_TITUL_NAME = "titul_id_titul";

        public static IEnumerable<int> GetTitulIds(int zamesId)
        {
            return GetIds(TABLE_NAME, TITUL_ID_TITUL_NAME, ZAMES_ID_ZAMES_NAME, zamesId);
        }

        public static IEnumerable<int> GetZamesIds(int titulId)
        {
            return GetIds(TABLE_NAME, ZAMES_ID_ZAMES_NAME, TITUL_ID_TITUL_NAME, titulId);
        }

        public static void InsertMapping(int zamesId, int titulId)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({ZAMES_ID_ZAMES_NAME}, {TITUL_ID_TITUL_NAME}) VALUES (:zamesId, :titulId)",
                new OracleParameter("zamesId", zamesId),
                new OracleParameter("titulId", titulId)
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
