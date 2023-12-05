using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;

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
                IdZamestnanec = int.Parse(query.Rows[0][ID_ZAMESTNANEC_NAME].ToString()),
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
        public static void UpsertZamestnanec(Zamestnanec zamestnanec)
        {
            OracleParameter idZamestnanecParam = new OracleParameter("idZamestnanec", OracleDbType.Int32, zamestnanec.IdZamestnanec, ParameterDirection.Input);
            OracleParameter jmenoParam = new OracleParameter("jmeno", OracleDbType.Varchar2, zamestnanec.Jmeno, ParameterDirection.Input);
            OracleParameter prijmeniParam = new OracleParameter("prijmeni", OracleDbType.Varchar2, zamestnanec.Prijmeni, ParameterDirection.Input);
            OracleParameter veterKlinIdParam = new OracleParameter("veterKlinId", OracleDbType.Int32, zamestnanec.VeterKlinId, ParameterDirection.Input);
            OracleParameter profeseParam = new OracleParameter("profese", OracleDbType.Varchar2, zamestnanec.Profese, ParameterDirection.Input);

            DatabaseController.Execute(
                $"pkg_ostatni.upsert_zamestnanec(:{ID_ZAMESTNANEC_NAME}, :{JMENO_NAME}, :{PRIJMENI_NAME}, :{VETER_KLIN_ID_NAME}, :{PROFES_NAME})",
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
                    IdZamestnanec = int.Parse(dr[ID_ZAMESTNANEC_NAME].ToString()),
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

