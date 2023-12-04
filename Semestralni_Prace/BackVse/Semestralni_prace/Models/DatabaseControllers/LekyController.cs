using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;

namespace Models.DatabaseControllers
{
    public class LekyController : Controller//TODO knihovna
    {
        public const string TABLE_NAME = "LEKY";
        public const string ID_NAME = "id_leky";
        public const string NAZEV_NAME = "nazev";

        public static IEnumerable<int> GetIds()
        {
            return GetIds(TABLE_NAME, ID_NAME);
        }

        public static Lek Get(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Lek()
            {
                Id = int.Parse(query.Rows[0][ID_NAME].ToString()),
                Nazev = query.Rows[0][NAZEV_NAME].ToString()
            };
        }

        public static void InsertLek(Lek lek)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({ID_NAME}, {NAZEV_NAME}) VALUES (:id, :nazev)",
                new OracleParameter("id", lek.Id),
                new OracleParameter("nazev", lek.Nazev)
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
        public static List<Lek> GetAll()
        {
            List<Lek> listLeky = new List<Lek>();
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");

            if (query.Rows.Count == 0)
            {
                return null;
            }

            foreach (DataRow row in query.Rows)
            {
                listLeky.Add(new Lek
                {
                    Id = int.Parse(row[ID_NAME].ToString()),
                    Nazev = row[NAZEV_NAME].ToString()
                });
            };
            return listLeky;
        }


    }
}


