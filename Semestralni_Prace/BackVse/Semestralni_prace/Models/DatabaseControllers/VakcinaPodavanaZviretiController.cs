using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Models.DatabaseControllers
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

       
        public static void UpsertVakcinaPodavanaZvireti(int vakcinaId, int zvireId)
        {
            OracleParameter vakcinaIdParam = new OracleParameter("vakcinaId", OracleDbType.Int32, vakcinaId, ParameterDirection.InputOutput);
            OracleParameter zvireIdParam = new OracleParameter("zvireId", OracleDbType.Int32, zvireId, ParameterDirection.InputOutput);

            DatabaseController.Execute(
                "pkg_ostatni.upsert_vakcina_podavana_zvireti",
                vakcinaIdParam,
                zvireIdParam
            );
        }

        public static void DeleteVakcinaPodavanaZvireti(int vakcinaId, int zvireId)
        {
            OracleParameter vakcinaIdParam = new OracleParameter("vakcinaId", OracleDbType.Int32, vakcinaId, ParameterDirection.Input);
            OracleParameter zvireIdParam = new OracleParameter("zvireId", OracleDbType.Int32, zvireId, ParameterDirection.Input);

            DatabaseController.Execute(
                $"DELETE FROM {TABLE_NAME} WHERE {VAKCINA_ID_NAME} = :vakcinaId AND {ZVIRE_ID_NAME} = :zvireId",
                vakcinaIdParam,
                zvireIdParam
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

        public static DataTable VakcinaPodavanaZviretiTable()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            return query;
        }


    }
}
