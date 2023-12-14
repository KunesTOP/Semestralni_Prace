using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Models.DatabaseControllers
{
    public class LekarUdavaAnamnezuController : Controller
    {
        public const string TABLE_NAME = "LEKAR_UDAVA_ANAMNEZU";
        public const string LEKAR_ID_NAME = "lekar_id_zamestnanec";
        public const string ANAMNEZA_ID_NAME = "anamneza_id_anamneza";

        public static IEnumerable<int> GetLekarIds(int anamnezaId)
        {
            return GetIds(TABLE_NAME, LEKAR_ID_NAME, ANAMNEZA_ID_NAME, anamnezaId);
        }

        public static IEnumerable<int> GetAnamnezaIds(int lekarId)
        {
            return GetIds(TABLE_NAME, ANAMNEZA_ID_NAME, LEKAR_ID_NAME, lekarId);
        }

       
        public static void DeleteMapping(int lekarId, int anamnezaId)
        {
            DatabaseController.Execute($"DELETE FROM {TABLE_NAME} WHERE {LEKAR_ID_NAME} = :lekarId AND {ANAMNEZA_ID_NAME} = :anamnezaId",
                new OracleParameter("lekarId", lekarId),
                new OracleParameter("anamnezaId", anamnezaId)
            );
        }

        public static void UpsertMapping(int lekarId, int anamnezaId)
        {
            OracleParameter lekarIdParam = new OracleParameter("p_lekar_id_zamestnanec", OracleDbType.Int32, ParameterDirection.InputOutput);
            lekarIdParam.Value = lekarId;

            OracleParameter anamnezaIdParam = new OracleParameter("p_anamneza_id_anamneza", OracleDbType.Int32, ParameterDirection.InputOutput);
            anamnezaIdParam.Value = anamnezaId;

            DatabaseController.Execute("pkg_ostatni.upsert_lekar_udava_anamnezu", lekarIdParam, anamnezaIdParam);
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
