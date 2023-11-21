using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_Práce.Classes;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class PrukazyController : Controller//TODO knihovna
    {
        public const string TABLE_NAME = "PRUKAZY";
        public const string CISLO_PRUKAZ_NAME = "cislo_prukaz";
        public const string CISLO_CHIP_NAME = "cislo_chip";
        public const string ID_PRUKAZ_NAME = "id_prukaz";
        public const string ZVIRE_ID_NAME = "zvire_id_zvire";

        public static IEnumerable<int> GetCisloPrukazIds()
        {
            return GetIds(TABLE_NAME, CISLO_PRUKAZ_NAME);
        }

        public static Prukaz GetByCisloPrukaz(int cisloPrukaz)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {CISLO_PRUKAZ_NAME} = :cisloPrukaz",
                new OracleParameter("cisloPrukaz", cisloPrukaz));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Prukaz()
            {
                CisloPrukaz = int.Parse(query.Rows[0][CISLO_PRUKAZ_NAME].ToString()),
                CisloChip = int.Parse(query.Rows[0][CISLO_CHIP_NAME].ToString()),
                IdPrukaz = int.Parse(query.Rows[0][ID_PRUKAZ_NAME].ToString()),
                ZvireId = query.Rows[0][ZVIRE_ID_NAME] == DBNull.Value ? null : (int?)int.Parse(query.Rows[0][ZVIRE_ID_NAME].ToString())
            };
        }

        public static void InsertPrukaz(Prukaz prukaz)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({CISLO_PRUKAZ_NAME}, {CISLO_CHIP_NAME}, {ID_PRUKAZ_NAME}, {ZVIRE_ID_NAME}) " +
                $"VALUES (:cisloPrukaz, :cisloChip, :idPrukaz, :zvireId)",
                new OracleParameter("cisloPrukaz", prukaz.CisloPrukaz),
                new OracleParameter("cisloChip", prukaz.CisloChip),
                new OracleParameter("idPrukaz", prukaz.IdPrukaz),
                new OracleParameter("zvireId", prukaz.ZvireId ?? (object)DBNull.Value)
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


