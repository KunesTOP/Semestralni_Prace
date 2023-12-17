using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace Models.DatabaseControllers
{
    public class ZamestnanciController 
    {
        public const string TABLE_NAME = "ZAMESTNANCI";
        public const string ID_ZAMESTNANEC_NAME = "id_zamestnanec";
        public const string JMENO_NAME = "jmeno";
        public const string PRIJMENI_NAME = "prijmeni";
        public const string VETER_KLIN_ID_NAME = "veter_klin_id";
        public const string PROFES_NAME = "profese";

        public static IEnumerable<int> GetIds()
        {
            return GetIds(TABLE_NAME, ID_ZAMESTNANEC_NAME);
        }

        public static Zamestnanec Get(int idZamestnanec)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_ZAMESTNANEC_NAME} = :idZamestnanec",
                new OracleParameter("idZamestnanec", idZamestnanec));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Zamestnanec()
            {
                Id = int.Parse(query.Rows[0][ID_ZAMESTNANEC_NAME].ToString()),
                Jmeno = query.Rows[0][JMENO_NAME].ToString(),
                Prijmeni = query.Rows[0][PRIJMENI_NAME].ToString(),
                VeterKlinId = int.Parse(query.Rows[0][VETER_KLIN_ID_NAME].ToString()),
                Profese = query.Rows[0][PROFES_NAME].ToString()
            };
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
        public static void UpsertZamestnanec(int id, JsonElement data)
        {
            OracleParameter idZamestnanecParam = new OracleParameter("p_id_zamestnanec", OracleDbType.Int32, ParameterDirection.InputOutput);
            idZamestnanecParam.Value = id;

            OracleParameter jmenoParam = new OracleParameter("p_jmeno", OracleDbType.Varchar2, data.GetProperty("jmeno").GetString(), ParameterDirection.Input);
            OracleParameter prijmeniParam = new OracleParameter("p_prijmeni", OracleDbType.Varchar2, data.GetProperty("prijmeni").GetString(), ParameterDirection.Input);

            OracleParameter veterKlinIdParam;
            if (data.TryGetProperty("VeterKlinId", out JsonElement veterKlinIdElement) && veterKlinIdElement.TryGetInt32(out int veterKlinId))
            {
                veterKlinIdParam = new OracleParameter("p_veter_klin_id", OracleDbType.Int32, veterKlinId, ParameterDirection.Input);
            }
            else
            {
                // Nastavte výchozí hodnotu na 1, pokud je VeterKlinId nepovinné
                veterKlinIdParam = new OracleParameter("p_veter_klin_id", OracleDbType.Int32, 1, ParameterDirection.Input);
            }
            OracleParameter profeseParam = new OracleParameter("p_profese", OracleDbType.Varchar2, data.GetProperty("profese").GetString(), ParameterDirection.Input);

            DatabaseController.Execute(
                "pkg_ostatni.upsert_zamestnanci",
                idZamestnanecParam,
                jmenoParam,
                prijmeniParam,
                veterKlinIdParam,
                profeseParam
            );
        }



        public static void DeleteZamestnanec(int idZamestnanec)
        {
            OracleParameter idZamestnanecParam = new OracleParameter("idZamestnanec", OracleDbType.Int32, idZamestnanec, ParameterDirection.Input);

            DatabaseController.Execute(
                $"pkg_delete.delete_zamestnanec(:{ID_ZAMESTNANEC_NAME})",
                idZamestnanecParam
            );
        }

        public static List<Zamestnanec> GetAll()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            List<Zamestnanec> listZamestnanci = new List<Zamestnanec>();
            if (query.Rows.Count == 0)
            {
                return null;
            }
            foreach (DataRow dr in query.Rows)
            {
                listZamestnanci.Add(new Zamestnanec()
                {
                    Id = int.Parse(dr[ID_ZAMESTNANEC_NAME].ToString()),
                    Jmeno = dr[JMENO_NAME].ToString(),
                    Prijmeni = dr[PRIJMENI_NAME].ToString(),
                    VeterKlinId = int.Parse(dr[VETER_KLIN_ID_NAME].ToString()),
                    Profese = dr[PROFES_NAME].ToString()
                });

            }
            return listZamestnanci;
        }
    }
}

