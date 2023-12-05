using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Models.DatabaseControllers
{
    public class ZvireMaNemocController 
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

     
        public static void UpsertZvireMaNemoc(int nemocId, int zvireId)
        {
            OracleParameter nemocIdParam = new OracleParameter("p_nemoc_nemoc_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            nemocIdParam.Value = nemocId;

            OracleParameter zvireIdParam = new OracleParameter("p_zvire_id_zvire", OracleDbType.Int32, ParameterDirection.InputOutput);
            zvireIdParam.Value = zvireId;

            DatabaseController.Execute("pkg_ostatni.upsert_zvire_ma_nemoc", nemocIdParam, zvireIdParam);
        }

        public static void DeleteMapping(int nemocId, int zvireId)
        {
            OracleParameter nemocIdParam = new OracleParameter("p_nemoc_nemoc_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            nemocIdParam.Value = nemocId;

            OracleParameter zvireIdParam = new OracleParameter("p_zvire_id_zvire", OracleDbType.Int32, ParameterDirection.InputOutput);
            zvireIdParam.Value = zvireId;
            DatabaseController.Execute("pkg_ostatni.delete_zvire_ma_nemoc", nemocIdParam, zvireIdParam
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
