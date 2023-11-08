using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using Semestralni_Práce.Classes;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class MajiteleZviratController //TODo knihovna
    {
        public const string TABLE_NAME = "MAJITELE_ZVIRAT";
        public const string PACIENT_ID_NAME = "id_pacient";
        public const string MAIL_NAME = "mail";
        public const string TELEFON_NAME = "telefon";
        public const string JMENO_NAME = "jmeno";
        public const string PRIJMENI_NAME = "prijmeni";
        public const string VET_KLIN_ID_NAME = "vet_klin_id";
        public const string MAJITEL_ID_NAME = "id_majitel";

        public static IEnumerable<int> GetPacientIds(int vetKlinId)
        {
            return GetIds(TABLE_NAME, PACIENT_ID_NAME, VET_KLIN_ID_NAME, vetKlinId);
        }

        public static Majitel Get(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {MAJITEL_ID_NAME} = :id",
                new OracleParameter("id", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Majitel()
            {
                PacientId = int.Parse(query.Rows[0][PACIENT_ID_NAME].ToString()),
                Mail = query.Rows[0][MAIL_NAME].ToString(),
                Telefon = query.Rows[0][TELEFON_NAME].ToString(),
                Jmeno = query.Rows[0][JMENO_NAME].ToString(),
                Prijmeni = query.Rows[0][PRIJMENI_NAME].ToString(),
                VetKlinId = int.Parse(query.Rows[0][VET_KLIN_ID_NAME].ToString()),
                IdMajitel = int.Parse(query.Rows[0][MAJITEL_ID_NAME].ToString())
            };
        }

        public static void InsertMajitel(Majitel majitel)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({PACIENT_ID_NAME}, {MAIL_NAME}, {TELEFON_NAME}, {JMENO_NAME}, {PRIJMENI_NAME}, {VET_KLIN_ID_NAME}, {MAJITEL_ID_NAME}) " +
                $"VALUES (:pacientId, :mail, :telefon, :jmeno, :prijmeni, :vetKlinId, :idMajitel)",
                new OracleParameter("pacientId", majitel.PacientId),
                new OracleParameter("mail", majitel.Mail),
                new OracleParameter("telefon", majitel.Telefon),
                new OracleParameter("jmeno", majitel.Jmeno),
                new OracleParameter("prijmeni", majitel.Prijmeni),
                new OracleParameter("vetKlinId", majitel.VetKlinId),
                new OracleParameter("idMajitel", majitel.IdMajitel)
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

