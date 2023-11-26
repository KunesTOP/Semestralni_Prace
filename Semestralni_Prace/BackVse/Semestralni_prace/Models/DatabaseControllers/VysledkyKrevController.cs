using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;

namespace Models.DatabaseControllers
{
    public class VysledkyKrevController : Controller//TODO KNIHOVNA
    {
        public const string TABLE_NAME = "VYSLEDKY_KREV";
        public const string ID_VYSLEDEK_NAME = "id_vysledek";
        public const string MNOZSTVI_PROTILATKY_NAME = "mnozstvi_protilatky";
        public const string MNOZSTVI_CERV_KRV_NAME = "mnozstvi_cerv_krv";
        public const string ANAMNEZA_ID_NAME = "anamneza_id";

        public static IEnumerable<int> GetAnamnezaIds()
        {
            return GetIds(TABLE_NAME, ANAMNEZA_ID_NAME);
        }

        public static VysledekKrev Get(int idVysledek)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_VYSLEDEK_NAME} = :idVysledek",
                new OracleParameter("idVysledek", idVysledek));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new VysledekKrev()
            {
                IdVysledek = int.Parse(query.Rows[0][ID_VYSLEDEK_NAME].ToString()),
                MnozstviProtilatky = int.Parse(query.Rows[0][MNOZSTVI_PROTILATKY_NAME].ToString()),
                MnozstviCervKrv = int.Parse(query.Rows[0][MNOZSTVI_CERV_KRV_NAME].ToString()),
                AnamnezaId = query.Rows[0][ANAMNEZA_ID_NAME] == DBNull.Value ? null : (int?)int.Parse(query.Rows[0][ANAMNEZA_ID_NAME].ToString())
            };
        }

        public static void InsertVysledekKrev(VysledekKrev vysledekKrev)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({ID_VYSLEDEK_NAME}, {MNOZSTVI_PROTILATKY_NAME}, {MNOZSTVI_CERV_KRV_NAME}, {ANAMNEZA_ID_NAME}) " +
                $"VALUES (:idVysledek, :mnozstviProtilatky, :mnozstviCervKrv, :anamnezaId)",
                new OracleParameter("idVysledek", vysledekKrev.IdVysledek),
                new OracleParameter("mnozstviProtilatky", vysledekKrev.MnozstviProtilatky),
                new OracleParameter("mnozstviCervKrv", vysledekKrev.MnozstviCervKrv),
                new OracleParameter("anamnezaId", vysledekKrev.AnamnezaId ?? (object)DBNull.Value)
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


