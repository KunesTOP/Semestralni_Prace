using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_Práce.Classes;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class ZamestnanciController : Controller//TODO KNIHOVNA
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
                IdZamestnanec = int.Parse(query.Rows[0][ID_ZAMESTNANEC_NAME].ToString()),
                Jmeno = query.Rows[0][JMENO_NAME].ToString(),
                Prijmeni = query.Rows[0][PRIJMENI_NAME].ToString(),
                VeterKlinId = int.Parse(query.Rows[0][VETER_KLIN_ID_NAME].ToString()),
                Profese = query.Rows[0][PROFES_NAME].ToString()
            };
        }

        public static void InsertZamestnanec(Zamestnanec zamestnanec)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({ID_ZAMESTNANEC_NAME}, {JMENO_NAME}, {PRIJMENI_NAME}, {VETER_KLIN_ID_NAME}, {PROFES_NAME}) " +
                $"VALUES (:idZamestnanec, :jmeno, :prijmeni, :veterKlinId, :profese)",
                new OracleParameter("idZamestnanec", zamestnanec.IdZamestnanec),
                new OracleParameter("jmeno", zamestnanec.Jmeno),
                new OracleParameter("prijmeni", zamestnanec.Prijmeni),
                new OracleParameter("veterKlinId", zamestnanec.VeterKlinId),
                new OracleParameter("profese", zamestnanec.Profese)
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

