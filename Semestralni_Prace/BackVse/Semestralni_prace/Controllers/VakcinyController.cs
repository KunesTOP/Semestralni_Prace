using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_Práce.Classes;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class VakcinyController : Controller//todo KNIHOVNY
    {
        public const string TABLE_NAME = "VAKCINY";
        public const string ID_VAKCINA_NAME = "id_vakcina";
        public const string NAZEV_VAKCINA_NAME = "nazev_vakcina";

        public static IEnumerable<int> GetVakcinaIds()
        {
            return GetIds(TABLE_NAME, ID_VAKCINA_NAME);
        }

        public static Vakcina GetByNazevVakcina(string nazevVakcina)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {NAZEV_VAKCINA_NAME} = :nazevVakcina",
                new OracleParameter("nazevVakcina", nazevVakcina));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Vakcina()
            {
                IdVakcina = int.Parse(query.Rows[0][ID_VAKCINA_NAME].ToString()),
                NazevVakcina = query.Rows[0][NAZEV_VAKCINA_NAME].ToString()
            };
        }

        public static void InsertVakcina(Vakcina vakcina)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({ID_VAKCINA_NAME}, {NAZEV_VAKCINA_NAME}) " +
                $"VALUES (:idVakcina, :nazevVakcina)",
                new OracleParameter("idVakcina", vakcina.IdVakcina),
                new OracleParameter("nazevVakcina", vakcina.NazevVakcina)
            );
        }
        public static void RemoveVakcina(int idVakcina)
        {
            DatabaseController.Execute($"DELETE FROM {TABLE_NAME} WHERE {ID_VAKCINA_NAME} = :idVakcina",
                new OracleParameter("idVakcina", idVakcina)
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


