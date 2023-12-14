using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace Models.DatabaseControllers//TODO KNIHOVNY
{
    public class VeterinarniKlinikaController 
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
                Id = int.Parse(query.Rows[0][VETER_KLIN_ID_NAME].ToString()),
                AdresyIdAdresa = int.Parse(query.Rows[0][ADRESY_ID_ADRESA_NAME].ToString())
            };
        }
        public static int? GetKlinikaIdByAdresa(string mesto, string ulice, int cisloPopisne)
        {
            try
            {
                string sqlQuery = $"SELECT vk.{VETER_KLIN_ID_NAME} FROM {TABLE_NAME} vk JOIN adresy a ON vk.{ADRESY_ID_ADRESA_NAME} = a.id_adresa WHERE a.mesto = :mesto AND a.ulice = :ulice AND a.cislo_popisne = :cisloPopisne";

                DataTable query = DatabaseController.Query(sqlQuery,
                    new OracleParameter("mesto", mesto),
                    new OracleParameter("ulice", ulice),
                    new OracleParameter("cisloPopisne", cisloPopisne));

                if (query.Rows.Count == 0)
                {
                    return null;
                }
                return int.Parse(query.Rows[0][VETER_KLIN_ID_NAME].ToString());
            }
            catch (OracleException ex)
            {
                // Handle the exception (e.g., log it or rethrow with a custom message)
                Console.WriteLine("Oracle Exception: " + ex.Message);
                return null;
            }
        }

        //TODO upravit aby to fungovalo - problém id adresy
        public static void UpsertKlinika(int id, JsonElement data)
        {
            Adresy aktualni = AdresyController.Get(id);
            if (aktualni.Equals(null)) { aktualni.Id = 1; }

            OracleParameter jmenoMajitelParam = new OracleParameter("jmenoMajitel", OracleDbType.Varchar2, data.GetProperty("jmenoMajitel").GetString(), ParameterDirection.Input);
            OracleParameter prijmeniMajitelParam = new OracleParameter("prijmeniMajitel", OracleDbType.Varchar2, data.GetProperty("prijmeniMajitel").GetString(), ParameterDirection.Input);
            OracleParameter veterKlinIdParam = new OracleParameter("veterKlinId", OracleDbType.Int32, id, ParameterDirection.Input);
            OracleParameter adresyIdAdresaParam = new OracleParameter("adresyIdAdresa", OracleDbType.Int32, aktualni.Id, ParameterDirection.Input);

            DatabaseController.Execute(
                $"pkg_ostatni.upsert_veterinarni_klinika(:{JMENO_MAJITEL_NAME}, :{PRIJMENI_MAJITEL_NAME}, :{VETER_KLIN_ID_NAME}, :{ADRESY_ID_ADRESA_NAME})",
                jmenoMajitelParam,
                prijmeniMajitelParam,
                veterKlinIdParam,
                adresyIdAdresaParam
            );
        }
        public static void DeleteKlinika(int veterKlinId)
        {
            OracleParameter veterKlinIdParam = new OracleParameter("veterKlinId", OracleDbType.Int32, veterKlinId, ParameterDirection.Input);

            DatabaseController.Execute(
                $"pkg_delete.upsert_veterinarni_klinika(:{VETER_KLIN_ID_NAME})",
                veterKlinIdParam
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

        public static IEnumerable<VeterinarniKlinika> GetAll()

        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");

            if (query.Rows.Count == 0)
            {
                return null;
            }
            List<VeterinarniKlinika> listKlinik = new List<VeterinarniKlinika>();
            foreach (DataRow dr in query.Rows)
            {
                listKlinik.Add(new VeterinarniKlinika()
                {
                    JmenoMajitel = dr[JMENO_MAJITEL_NAME].ToString(),
                    PrijmeniMajitel = dr[PRIJMENI_MAJITEL_NAME].ToString(),
                    Id = int.Parse(dr[VETER_KLIN_ID_NAME].ToString()),
                    AdresyIdAdresa = int.Parse(dr[ADRESY_ID_ADRESA_NAME].ToString())
                });
            }
            return listKlinik;
        }
    }
}


