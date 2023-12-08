using Back.databaze;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Data;
using System.Reflection.Metadata;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Semestralni_prace.Models.DatabaseControllers
{
    public class DokumentController
    {
        private const string TABLE_NAME = "DOKUMENTY";
        private const string ID_DOKUMENTY_NAME = "ID_DOKUMENT";
        private const string PRIPOMA_DOKUMENTY_NAME = "PRIPONA";
        private const string DATA_DOKUMENT_NAME = "DOKUMENT";
        private const string DOKUMENT_NAME = "NAZEV_SOUBORU";

        //TODO: Zkontrolovat, jestli todle je správně
        public static Dokument Get(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_DOKUMENTY_NAME} = :id",
                new OracleParameter("id", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Dokument()
            {
                IdDokument = int.Parse(query.Rows[0][ID_DOKUMENTY_NAME].ToString()),
                Pripona = query.Rows[0][PRIPOMA_DOKUMENTY_NAME].ToString(),
                Data = query.Rows[0][DATA_DOKUMENT_NAME].ToString(),
                DokumentNazev = query.Rows[0][DOKUMENT_NAME].ToString()
            };
        }
        public static void Upsert(Dokument dokument)
        {
            OracleParameter idParam = new OracleParameter("p_id_dokument", OracleDbType.Int32, ParameterDirection.InputOutput);
            idParam.Value = dokument.IdDokument;

            OracleParameter priponaParam = new OracleParameter("p_pripoma_dokumenty", OracleDbType.Varchar2, ParameterDirection.InputOutput);
            priponaParam.Value = dokument.Pripona;

            OracleParameter dataParam = new OracleParameter("p_data_dokument", OracleDbType.Blob, ParameterDirection.InputOutput);
            dataParam.Value = dokument.DokumentData;

            OracleParameter nazevParam = new OracleParameter("p_dokument_nazev", OracleDbType.Varchar2, ParameterDirection.InputOutput);
            nazevParam.Value = dokument.DokumentNazev;

            DatabaseController.Execute("pkg_dokumenty.upsert_dokumenty", idParam, priponaParam, dataParam, nazevParam);
        }
        public static void Delete(int id)
        {
            DatabaseController.Execute($"DELETE FROM {TABLE_NAME} WHERE {ID_DOKUMENTY_NAME} = :id",
                new OracleParameter("id", id)
            );
        }
        public static IEnumerable<Dokument> GetAll()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");

            if (query.Rows.Count == 0)
            {
                return null;
            }
            List<Dokument> listDokumentu = new List<Dokument>();
            foreach (DataRow row in query.Rows)
            {
                listDokumentu.Add(new Dokument()
                {
                    IdDokument = int.Parse(query.Rows[0][ID_DOKUMENTY_NAME].ToString()),
                    Pripona = query.Rows[0][PRIPOMA_DOKUMENTY_NAME].ToString(),
                    Data = query.Rows[0][DATA_DOKUMENT_NAME].ToString(),
                    DokumentNazev = query.Rows[0][DOKUMENT_NAME].ToString()
                });
            }
            return listDokumentu;
        }
        public static void Delete(int id)
        {
            DatabaseController.Execute($"DELETE FROM {TABLE_NAME} WHERE {ID_DOKUMENTY_NAME} = :id",
                new OracleParameter("id", id)
            );
        }

        public static void UpsertDokument(Dokument doc)
        {
            OracleParameter idParam = new OracleParameter("p_ID_dokument", OracleDbType.Int32, ParameterDirection.InputOutput);
            idParam.Value = doc.IdDokument;

            OracleParameter nazevParam = new OracleParameter("p_Nazev_souboru", OracleDbType.Varchar2, ParameterDirection.Input);
            nazevParam.Value = doc.DokumentNazev;

            OracleParameter pripParam = new OracleParameter("p_pripona", OracleDbType.Varchar2, ParameterDirection.Input);
            pripParam.Value = doc.Pripona;

            OracleParameter dataParam = new OracleParameter("p_data", OracleDbType.Blob, ParameterDirection.Input)
            {
                Value = doc.GetBytes()
            };

            DatabaseController.Execute("pkg_dokumenty.upsert_dokumenty", idParam, pripParam,nazevParam,dataParam);
        }
    }
}
