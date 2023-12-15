using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Models.DatabaseControllers
{
    public class MajitelVlastniPrukazController 
    {
        public const string TABLE_NAME = "MAJITEL_VLASTNI_PRUKAZ";
        public const string MAJITEL_ID_NAME = "majitel_zvire_id_pacient";
        public const string PRUKAZ_ID_NAME = "prukaz_id_prukaz";

        public static IEnumerable<int> GetMajitelIds(int prukazId)
        {
            return GetIds(TABLE_NAME, MAJITEL_ID_NAME, PRUKAZ_ID_NAME, prukazId);
        }

        public static IEnumerable<int> GetPrukazIds(int majitelId)
        {
            return GetIds(TABLE_NAME, PRUKAZ_ID_NAME, MAJITEL_ID_NAME, majitelId);
        }

       
        public static void DeleteMapping(int majitelId, int prukazId)
        {
            DatabaseController.Execute1($"DELETE FROM {TABLE_NAME} WHERE {MAJITEL_ID_NAME} = :majitelId AND {PRUKAZ_ID_NAME} = :prukazId",
                new OracleParameter("majitelId", majitelId),
                new OracleParameter("prukazId", prukazId)
            );
        }

        public static void UpsertMapping(int majitelId, int prukazId)
        {
            OracleParameter majitelIdParam = new OracleParameter("p_majitel_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            majitelIdParam.Value = majitelId;

            OracleParameter prukazIdParam = new OracleParameter("p_prukaz_id", OracleDbType.Int32, ParameterDirection.Input);
            prukazIdParam.Value = prukazId;

            DatabaseController.Execute("pkg_ostatni.upsert_majitel_vlastni_prukaz", majitelIdParam, prukazIdParam);
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
