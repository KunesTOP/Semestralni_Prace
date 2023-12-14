using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Models.DatabaseControllers
{
    public class AnamnezaUrCujeNemocController 
    {
        public const string TABLE_NAME = "ANAMNEZA_URCUJE_NEMOC";
        public const string NEMOC_ID_NAME = "nemoc_nemoc_id";
        public const string ANAMNEZA_ID_NAME = "anamneza_id_anamneza";

        public static IEnumerable<int> GetNemocIds(int anamnezaId)
        {
            return GetIds(TABLE_NAME, NEMOC_ID_NAME, ANAMNEZA_ID_NAME, anamnezaId);
        }

        public static IEnumerable<int> GetAnamnezaIds(int nemocId)
        {
            return GetIds(TABLE_NAME, ANAMNEZA_ID_NAME, NEMOC_ID_NAME, nemocId);
        }

        public static void UpsertMapping(int nemocId, int anamnezaId)
        {
            // Vytvoření parametrů pro stored proceduru
            OracleParameter nemocIdParam = new OracleParameter("p_nemoc_id", OracleDbType.Int32, ParameterDirection.Input);
            nemocIdParam.Value = nemocId;

            OracleParameter anamnezaIdParam = new OracleParameter("p_anamneza_id", OracleDbType.Int32, ParameterDirection.Input);
            anamnezaIdParam.Value = anamnezaId;

            // Volání stored procedury pro upsert
            DatabaseController.Execute1("pkg_ostatni.upsert_anamneza_urcuje_nemoc", nemocIdParam, anamnezaIdParam);
        }

        public static void DeleteMapping(int nemocId, int anamnezaId)
        {
            DatabaseController.Execute($"DELETE FROM {TABLE_NAME} WHERE {NEMOC_ID_NAME} = :nemocId AND {ANAMNEZA_ID_NAME} = :anamnezaId",
                new OracleParameter("nemocId", nemocId),
                new OracleParameter("anamnezaId", anamnezaId)
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
