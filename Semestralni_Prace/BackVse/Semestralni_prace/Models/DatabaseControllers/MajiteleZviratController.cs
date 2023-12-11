using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace Models.DatabaseControllers
{
    public class MajiteleZviratController
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


        public static void DeleteMajitel(int id)
        {
            DatabaseController.Execute($"DELETE FROM {TABLE_NAME} WHERE {MAJITEL_ID_NAME} = :id",
                new OracleParameter("id", id)
            );
        }
        //TODO vyřešit tendle upsert
        public static void UpsertMajitel(int id, JsonElement data)
        {
            Majitel aktualni = MajiteleZviratController.Get(id);
            if (aktualni.Equals(null))
            {
                aktualni.VetKlinId = 1;
                aktualni.IdMajitel = 1;
            } 

            OracleParameter pacientIdParam = new OracleParameter("p_pacient_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            pacientIdParam.Value = id;

            OracleParameter mailParam = new OracleParameter("p_mail", OracleDbType.Varchar2, ParameterDirection.Input);
            mailParam.Value = data.GetProperty("mail").GetString();

            OracleParameter telefonParam = new OracleParameter("p_telefon", OracleDbType.Varchar2, ParameterDirection.Input);
            telefonParam.Value = data.GetProperty("telefon").GetString().Replace(" ", string.Empty);

            OracleParameter jmenoParam = new OracleParameter("p_jmeno", OracleDbType.Varchar2, ParameterDirection.Input);
            jmenoParam.Value = data.GetProperty("jmeno").GetString();

            OracleParameter prijmeniParam = new OracleParameter("p_prijmeni", OracleDbType.Varchar2, ParameterDirection.Input);
            prijmeniParam.Value = data.GetProperty("prijmeni").GetString();

            OracleParameter vetKlinIdParam = new OracleParameter("p_vet_klin_id", OracleDbType.Int32, ParameterDirection.Input);
            vetKlinIdParam.Value = aktualni.VetKlinId;

            OracleParameter idMajitelParam = new OracleParameter("p_id_majitel", OracleDbType.Int32, ParameterDirection.Input);
            idMajitelParam.Value = aktualni.IdMajitel;

            DatabaseController.Execute("pkg_model_dml1.upsert_majitel", pacientIdParam, mailParam, telefonParam, jmenoParam, prijmeniParam, vetKlinIdParam);
        }
        public static int UpsertMajitelPacient(int idKlinika,JsonElement data)
        {
            OracleParameter pacientIdParam = new OracleParameter("p_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            pacientIdParam.Value = -1;

            OracleParameter mailParam = new OracleParameter("p_mail", OracleDbType.Varchar2, ParameterDirection.Input);
            mailParam.Value = data.GetProperty("email").GetString();

            OracleParameter telefonParam = new OracleParameter("p_telefon", OracleDbType.Varchar2, ParameterDirection.Input);
            telefonParam.Value = data.GetProperty("telefon").GetString().Replace(" ",string.Empty);

            OracleParameter jmenoParam = new OracleParameter("p_jmeno", OracleDbType.Varchar2, ParameterDirection.Input);
            jmenoParam.Value = data.GetProperty("krestni").GetString();

            OracleParameter prijmeniParam = new OracleParameter("p_prijmeni", OracleDbType.Varchar2, ParameterDirection.Input);
            prijmeniParam.Value = data.GetProperty("prijmeni").GetString();

            OracleParameter vetKlinIdParam = new OracleParameter("p_vet_klin_id", OracleDbType.Int32, ParameterDirection.Input);
            vetKlinIdParam.Value = idKlinika;

            OracleParameter idMajitelParam = new OracleParameter("p_id_majitel", OracleDbType.Int32, ParameterDirection.Input);
            idMajitelParam.Value = idKlinika;

            DatabaseController.Execute("pkg_model_dml1.upsert_majitel", pacientIdParam, mailParam, telefonParam, jmenoParam, prijmeniParam, vetKlinIdParam);
            return int.Parse((pacientIdParam.Value).ToString());
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

        public static List<Majitel> GetAll()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            if (query.Rows.Count == 0)
            {
                return null;
            }
            List<Majitel> majitele = new List<Majitel>();

            foreach (DataRow dr in query.Rows)
                majitele.Add(new Majitel()
                {
                    PacientId = int.Parse(dr[PACIENT_ID_NAME].ToString()),
                    Mail = dr[MAIL_NAME].ToString(),
                    Telefon = dr[TELEFON_NAME].ToString(),
                    Jmeno = dr[JMENO_NAME].ToString(),
                    Prijmeni = dr[PRIJMENI_NAME].ToString(),
                    VetKlinId = int.Parse(dr[VET_KLIN_ID_NAME].ToString()),
                    IdMajitel = int.Parse(dr[MAJITEL_ID_NAME].ToString())
                });
            return majitele;
        }
    }
}

