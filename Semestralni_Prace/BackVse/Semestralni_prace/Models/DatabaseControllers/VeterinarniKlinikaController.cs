using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;

namespace Models.DatabaseControllers//TODO KNIHOVNY
{
    public class VeterinarniKlinikaController : Controller
    {
        public const string TABLE_NAME = "VETERINARNI_KLINIKA";
        public const string JMENO_MAJITEL_NAME = "jmeno_majitel";
        public const string PRIJMENI_MAJITEL_NAME = "prijmeni_majitel";
        public const string VETER_KLIN_ID_NAME = "veter_klin_id";
        public const string ADRESY_ID_ADRESA_NAME = "adresy_id_adresa";

        public static IEnumerable<int> GetAdresaIds(int veterKlinId)
        {
            return GetIds(TABLE_NAME, ADRESY_ID_ADRESA_NAME, VETER_KLIN_ID_NAME, veterKlinId);
        }

       private static VeterinarniKlinika Get(int veterKlinId)//TODO ??

        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {VETER_KLIN_ID_NAME} = :veterKlinId",
                new OracleParameter("veterKlinId", veterKlinId));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new VeterinarniKlinika()
            {
                JmenoMajitel = query.Rows[0][JMENO_MAJITEL_NAME].ToString(),
                PrijmeniMajitel = query.Rows[0][PRIJMENI_MAJITEL_NAME].ToString(),
                VeterKlinId = int.Parse(query.Rows[0][VETER_KLIN_ID_NAME].ToString()),
                AdresyIdAdresa = int.Parse(query.Rows[0][ADRESY_ID_ADRESA_NAME].ToString())
            };
        }

        public static void InsertKlinika(VeterinarniKlinika klinika)//TODO
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({JMENO_MAJITEL_NAME}, {PRIJMENI_MAJITEL_NAME}, {VETER_KLIN_ID_NAME}, {ADRESY_ID_ADRESA_NAME}) " +
                $"VALUES (:jmenoMajitel, :prijmeniMajitel, :veterKlinId, :adresyIdAdresa)",
                new OracleParameter("jmenoMajitel", klinika.JmenoMajitel),
                new OracleParameter("prijmeniMajitel", klinika.PrijmeniMajitel),
                new OracleParameter("veterKlinId", klinika.VeterKlinId),
                new OracleParameter("adresyIdAdresa", klinika.AdresyIdAdresa)
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


