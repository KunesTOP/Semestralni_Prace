using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Models.DatabaseControllers
{
    public class AnamnezyController
    {
        public const string TABLE_NAME = "ANAMNEZY";
        public const string ID_NAME = "id_anamneza";
        public const string DATUM_NAME = "datum_anamneza";

        public static IEnumerable<int> GetIds()
        {
            return GetIds(TABLE_NAME, ID_NAME);
        }

        public static Anamneza Get(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Anamneza()
            {
                Id = int.Parse(query.Rows[0][ID_NAME].ToString()),
                Datum = DateTime.Parse(query.Rows[0][DATUM_NAME].ToString())
            };
        }

        public static List<Anamneza> GetAll()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            List<Anamneza> zaznamy = new List<Anamneza>();

            if (query.Rows.Count == 0)
            {
                return null;
            }

            foreach (DataRow dr in query.Rows)
            {
                zaznamy.Add(new Anamneza
                {
                    Id = int.Parse(dr[ID_NAME].ToString()),
                    Datum = DateTime.Parse(dr[DATUM_NAME].ToString())
                });
            }

            return zaznamy;
        }

        public static void UpdateZaznamAnamnezy(Anamneza zaznam)
        {
            DatabaseController.Execute($"UPDATE {TABLE_NAME} SET {DATUM_NAME} = :datum WHERE {ID_NAME} = :id",
                new OracleParameter("id", zaznam.Id),
                new OracleParameter("datum", zaznam.Datum)
            );
        }

        public static void DeleteZaznamAnamnezy(int id)
        {
            DatabaseController.Execute($"DELETE FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id)
            );
        }

        private static IEnumerable<int> GetIds(string tableName, string idColumnName)
        {
            List<int> ids = new List<int>();

            DataTable query = DatabaseController.Query($"SELECT {idColumnName} FROM {tableName}");

            foreach (DataRow dr in query.Rows)
            {
                ids.Add(int.Parse(dr[idColumnName].ToString()));
            }

            return ids;
        }
    }
}
