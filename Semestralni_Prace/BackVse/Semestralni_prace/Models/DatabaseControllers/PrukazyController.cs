using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace Models.DatabaseControllers
{
    public class PrukazyController 
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
        public static void DeletePrukaz(int cisloPrukaz)
        {
            DatabaseController.Execute($"DELETE FROM {TABLE_NAME} WHERE {CISLO_PRUKAZ_NAME} = :cisloPrukaz",
                new OracleParameter("cisloPrukaz", cisloPrukaz)
            );
        }

        public static void UpsertPrukaz(int id, JsonElement data)
        {
            Prukaz aktualni = PrukazyController.GetByCisloPrukaz(data.GetProperty("cisloPrukaz").GetInt32());
            if(aktualni.Equals(null)) { aktualni.ZvireId = null; }

            OracleParameter cisloPrukazParam = new OracleParameter("p_cislo_prukaz", OracleDbType.Int32, ParameterDirection.InputOutput);
            cisloPrukazParam.Value = data.GetProperty("cisloPrukaz").GetInt32();

            OracleParameter cisloChipParam = new OracleParameter("p_cislo_chip", OracleDbType.Int32, ParameterDirection.Input);
            cisloChipParam.Value = data.GetProperty("cisloChip").GetInt32();

            OracleParameter idPrukazParam = new OracleParameter("p_id_prukaz", OracleDbType.Int32, ParameterDirection.Input);
            idPrukazParam.Value = id;

            OracleParameter zvireIdParam = new OracleParameter("p_zvire_id", OracleDbType.Int32, ParameterDirection.Input);
            zvireIdParam.Value = aktualni.ZvireId ?? (object)DBNull.Value;

            DatabaseController.Execute("pkg_ostatni.upsert_prukazy", cisloPrukazParam, cisloChipParam, idPrukazParam, zvireIdParam);
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

        public static List<Prukaz> GetAll()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");

            if (query.Rows.Count == 0)
            {
                return null;
            }
            List<Prukaz> prukazy = new List<Prukaz>();
            foreach (DataRow dr in query.Rows)
            {
                prukazy.Add(new Prukaz()
                {
                    CisloPrukaz = int.Parse(dr[CISLO_PRUKAZ_NAME].ToString()),
                    CisloChip = int.Parse(dr[CISLO_CHIP_NAME].ToString()),
                    IdPrukaz = int.Parse(dr[ID_PRUKAZ_NAME].ToString()),
                    ZvireId = dr[ZVIRE_ID_NAME] == DBNull.Value ? null : (int?)int.Parse(dr[ZVIRE_ID_NAME].ToString())
                });
            }
            return prukazy;
        }
    }
}


