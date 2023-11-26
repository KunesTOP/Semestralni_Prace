using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Models.DatabaseControllers
{
    public class AnamnezaUrCujeNemocController : Controller //todo zkoukni radsi vsechny potom se bude urcovat smer tak na to jen mrkni kdyztak budou nejaky todo specificke vetsinou
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

        public static void InsertMapping(int nemocId, int anamnezaId)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({NEMOC_ID_NAME}, {ANAMNEZA_ID_NAME}) VALUES (:nemocId, :anamnezaId)",
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
