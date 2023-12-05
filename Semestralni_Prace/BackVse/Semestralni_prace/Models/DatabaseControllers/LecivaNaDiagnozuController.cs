using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Models.DatabaseControllers
{
    public class LecivaNaDiagnozuController 
    {
        public const string TABLE_NAME = "LECIVA_NA_DIAGNOZU";
        public const string LEKY_ID_NAME = "leky_id_leky";
        public const string ANAMN_ID_NAME = "anamn_id_anamn";

        public static IEnumerable<int> GetLekyIds(int anamnId)
        {
            return GetIds(TABLE_NAME, LEKY_ID_NAME, ANAMN_ID_NAME, anamnId);
        }

        public static IEnumerable<int> GetAnamnIds(int lekyId)
        {
            return GetIds(TABLE_NAME, ANAMN_ID_NAME, LEKY_ID_NAME, lekyId);
        }

      
        public static void DeleteLecivaNaDiagnozu(int lekyId, int anamnId)
        {
            DatabaseController.Execute($"DELETE FROM {TABLE_NAME} WHERE {LEKY_ID_NAME} = :lekyId AND {ANAMN_ID_NAME} = :anamnId",
                new OracleParameter("lekyId", lekyId),
                new OracleParameter("anamnId", anamnId)
            );
        }

        public static void UpsertLecivaNaDiagnozu(int lekyId, int anamnId)
        {
            OracleParameter lekyIdParam = new OracleParameter("p_leky_id_leky", OracleDbType.Int32, ParameterDirection.InputOutput);
            lekyIdParam.Value = lekyId;

            OracleParameter anamnIdParam = new OracleParameter("p_anamn_id_anamn", OracleDbType.Int32, ParameterDirection.InputOutput);
            anamnIdParam.Value = anamnId;

            DatabaseController.Execute("pkg_ostatni.upsert_leciva_na_diagnozu", lekyIdParam, anamnIdParam);
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
