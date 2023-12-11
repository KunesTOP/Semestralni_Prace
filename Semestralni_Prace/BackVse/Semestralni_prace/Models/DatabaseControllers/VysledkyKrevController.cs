using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

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

        public static void UpsertVysledekKrev(int id, JsonElement data)
        {
            VysledekKrev aktualni = VysledkyKrevController.Get(id);
            if (aktualni.Equals(null)) { aktualni.AnamnezaId = 1; }

            OracleParameter idVysledekParam = new OracleParameter("idVysledek", OracleDbType.Int32, id, ParameterDirection.Input);
            OracleParameter mnozstviProtilatkyParam = new OracleParameter("mnozstviProtilatky", OracleDbType.Int32, data.GetProperty("mnnozstviProtilatky").GetString(), ParameterDirection.Input);
            OracleParameter mnozstviCervKrvParam = new OracleParameter("mnozstviCervKrv", OracleDbType.Int32, data.GetProperty("mnozstviCervKrv").GetString(), ParameterDirection.Input);
            OracleParameter anamnezaIdParam = new OracleParameter("anamnezaId", OracleDbType.Int32, aktualni.AnamnezaId ?? (object)DBNull.Value, ParameterDirection.Input);

            DatabaseController.Execute(
                $"pkg_ostatni.upsert_vysledek_krev(:{ID_VYSLEDEK_NAME}, :{MNOZSTVI_PROTILATKY_NAME}, :{MNOZSTVI_CERV_KRV_NAME}, :{ANAMNEZA_ID_NAME})",
                idVysledekParam,
                mnozstviProtilatkyParam,
                mnozstviCervKrvParam,
                anamnezaIdParam
            );
        }
        public static void DeleteVysledekKrev(int idVysledek)
        {
            OracleParameter idVysledekParam = new OracleParameter("idVysledek", OracleDbType.Int32, idVysledek, ParameterDirection.Input);

            DatabaseController.Execute(
                $"pkg_delete.delete_vysledek_krev(:{ID_VYSLEDEK_NAME})",
                idVysledekParam
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
        public static IEnumerable<VysledekKrev> GetAll()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");

            if (query.Rows.Count == 0)
            {
                return null;
            }
            List<VysledekKrev> listVysledku = new List<VysledekKrev>();
            foreach (DataRow dr in query.Rows)
            {
                listVysledku.Add(new VysledekKrev()
                {
                    IdVysledek = int.Parse(dr[ID_VYSLEDEK_NAME].ToString()),
                    MnozstviProtilatky = int.Parse(dr[MNOZSTVI_PROTILATKY_NAME].ToString()),
                    MnozstviCervKrv = int.Parse(dr[MNOZSTVI_CERV_KRV_NAME].ToString()),
                    AnamnezaId = dr[ANAMNEZA_ID_NAME] == DBNull.Value ? null : (int?)int.Parse(dr[ANAMNEZA_ID_NAME].ToString())
                });
            }
            return listVysledku;
        }
    }
}


