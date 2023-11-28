using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;

namespace Models.DatabaseControllers
{
    public class AsistentiController : Controller//TODO knihovna dole 
    {
        public const string TABLE_NAME = "ASISTENTI";
        public const string ID_NAME = "id_zamestnanec";
        public const string PRAXE_NAME = "praxe";

        public static IEnumerable<int> GetIds()
        {
            return GetIds(TABLE_NAME, ID_NAME);
        }

        public static Asistent Get(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Asistent()
            {
                IdZamestnanec = int.Parse(query.Rows[0][ID_NAME].ToString()),
                Praxe = int.Parse(query.Rows[0][PRAXE_NAME].ToString())
            };
        }

        public static void InsertAsistent(Asistent asistent)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({ID_NAME}, {PRAXE_NAME}) VALUES (:id, :praxe)",
                new OracleParameter("id", asistent.IdZamestnanec),
                new OracleParameter("praxe", asistent.Praxe)
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

        public static List<Asistent> GetAll()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            List<Asistent> asistent = new List<Asistent>();

            if (query.Rows.Count == 0)
            {
                return null;
            }
            try
            {
                foreach (DataRow dr in query.Rows)
                {
                    asistent.Add(new Asistent()
                    {
                        IdZamestnanec = int.Parse(dr[ID_NAME].ToString()),
                        Praxe = int.Parse(dr[PRAXE_NAME].ToString())
                    });
                }
            }
            catch (Exception ex)
            {

            }
            return asistent;
        }
    }
}


