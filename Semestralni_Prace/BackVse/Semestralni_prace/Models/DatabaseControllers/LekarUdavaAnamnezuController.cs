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

        public static void InsertMapping(int lekarId, int anamnezaId)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({LEKAR_ID_NAME}, {ANAMNEZA_ID_NAME}) VALUES (:lekarId, :anamnezaId)",
                new OracleParameter("lekarId", lekarId),
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
