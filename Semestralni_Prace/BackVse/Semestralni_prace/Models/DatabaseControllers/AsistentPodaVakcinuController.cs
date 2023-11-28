﻿using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Models.DatabaseControllers
{
    public class AsistentPodaVakcinuController : Controller
    {
        public const string TABLE_NAME = "ASISTENT_PODA_VAKCINU";
        public const string ASISTENT_ID_NAME = "asistent_id_zamestnanec";
        public const string VAKCINA_ID_NAME = "vakcina_id_vakcina";

        public static IEnumerable<int> GetAsistentIds(int vakcinaId)
        {
            return GetIds(TABLE_NAME, ASISTENT_ID_NAME, VAKCINA_ID_NAME, vakcinaId);
        }

        public static IEnumerable<int> GetVakcinaIds(int asistentId)
        {
            return GetIds(TABLE_NAME, VAKCINA_ID_NAME, ASISTENT_ID_NAME, asistentId);
        }

        public static void InsertMapping(int asistentId, int vakcinaId)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({ASISTENT_ID_NAME}, {VAKCINA_ID_NAME}) VALUES (:asistentId, :vakcinaId)",
                new OracleParameter("asistentId", asistentId),
                new OracleParameter("vakcinaId", vakcinaId)
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
        private static DataTable AsistentPodavaVakcinuTable()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            return query;
        }

    }
}
