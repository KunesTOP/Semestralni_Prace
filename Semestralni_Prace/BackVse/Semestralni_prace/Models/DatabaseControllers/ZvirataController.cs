using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace Models.DatabaseControllers
{
    public class ZvirataController
    {
        public const string TABLE_NAME = "ZVIRATA";
        public const string JMENO_ZVIRE_NAME = "jmeno_zvire";
        public const string POHLAVI_NAME = "pohlavi";
        public const string DATUM_NAROZENI_NAME = "datum_narozeni";
        public const string DATUM_UMRTI_NAME = "datum_umrti";
        public const string MAJITEL_ZVIRE_ID_PACIENT_NAME = "majitel_zvire_id_pacient";
        public const string ID_ZVIRE_NAME = "id_zvire";
        public const string RASA_ZVIRAT_ID_RASA_NAME = "rasa_zvirat_id_rasa";

        public static IEnumerable<int> GetMajitelIds(int zvireId)
        {
            return GetIds(TABLE_NAME, MAJITEL_ZVIRE_ID_PACIENT_NAME, ID_ZVIRE_NAME, zvireId);
        }

        public static Zvire Get(int idZvire)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_ZVIRE_NAME} = :idZvire",
                new OracleParameter("idZvire", idZvire));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Zvire()
            {
                JmenoZvire = query.Rows[0][JMENO_ZVIRE_NAME].ToString(),
                Pohlavi = query.Rows[0][POHLAVI_NAME].ToString(),
                DatumNarozeni = DateTime.Parse(query.Rows[0][DATUM_NAROZENI_NAME].ToString()),
                DatumUmrti = query.Rows[0][DATUM_UMRTI_NAME] == DBNull.Value ? null : (DateTime?)DateTime.Parse(query.Rows[0][DATUM_UMRTI_NAME].ToString()),
                MajitelZvireIdPacient = int.Parse(query.Rows[0][MAJITEL_ZVIRE_ID_PACIENT_NAME].ToString()),
                Id = int.Parse(query.Rows[0][ID_ZVIRE_NAME].ToString()),
                RasaZviratIdRasa = int.Parse(query.Rows[0][RASA_ZVIRAT_ID_RASA_NAME].ToString())
            };
        }

        public static int InsertZvire(
            string jmeno,
            string pohlavi,
            DateTime datumNarozeni,
            DateTime? datumUmrti,
            int majitelId,
            int idZvire,
            int rasaId)
        {
            OracleParameter jmenoParam = new OracleParameter("p_jmeno", OracleDbType.Varchar2, jmeno, ParameterDirection.Input);
            OracleParameter pohlaviParam = new OracleParameter("p_pohlavi", OracleDbType.Varchar2, pohlavi, ParameterDirection.Input);
            OracleParameter datumNarozeniParam = new OracleParameter("p_datum_narozeni", OracleDbType.Date, datumNarozeni, ParameterDirection.Input);
            OracleParameter datumUmrtiParam = new OracleParameter("p_datum_umrti", OracleDbType.Date, datumUmrti ?? (object)DBNull.Value, ParameterDirection.Input);
            OracleParameter majitelIdParam = new OracleParameter("p_majitel_zvire_id_pacient", OracleDbType.Int32, majitelId, ParameterDirection.Input);
            OracleParameter idZvireParam = new OracleParameter("p_id_zvire", OracleDbType.Int32, idZvire, ParameterDirection.Input);
            OracleParameter rasaIdParam = new OracleParameter("p_rasa_zvirat_id_rasa", OracleDbType.Int32, rasaId, ParameterDirection.Input);

            OracleParameter resultParam = new OracleParameter("result", OracleDbType.Int32, ParameterDirection.ReturnValue);

            DatabaseController.Execute(
                "pkg_model_dml1.insert_zvire",
                resultParam,
                jmenoParam,
                pohlaviParam,
                datumNarozeniParam,
                datumUmrtiParam,
                majitelIdParam,
                idZvireParam,
                rasaIdParam
            );

            return int.Parse(resultParam.Value.ToString());
        }
        public static void UpsertZvire(int id, JsonElement data)
        {
            Zvire aktualni = ZvirataController.Get(id);
            if (aktualni.Equals(null)) { aktualni.MajitelZvireIdPacient = 1; aktualni.RasaZviratIdRasa = 1; }

            OracleParameter zvireIdParam = new OracleParameter("p_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            zvireIdParam.Value = id;

            OracleParameter jmenoParam = new OracleParameter("p_jmeno", OracleDbType.Varchar2, ParameterDirection.Input);
            jmenoParam.Value = data.GetProperty("jmenoZvire").GetString();

            OracleParameter pohlaviParam = new OracleParameter("p_pohlavi", OracleDbType.Varchar2, ParameterDirection.Input);
            pohlaviParam.Value = data.GetProperty("pohlavi").GetString();

            OracleParameter narozeniParam = new OracleParameter("p_datum_narozeni", OracleDbType.Date, ParameterDirection.Input);
            narozeniParam.Value = data.GetProperty("datumNarozeni").GetString();

            OracleParameter umrtiParam = new OracleParameter("p_datum_umrti", OracleDbType.Date, ParameterDirection.Input);
            umrtiParam.Value = data.GetProperty("datumUmrti").GetString() ?? (object)DBNull.Value;

            OracleParameter majitelZvireteIdParam = new OracleParameter("p_majitel_zvire_id_pacient", OracleDbType.Int32, ParameterDirection.Input);
            majitelZvireteIdParam.Value = aktualni.MajitelZvireIdPacient;

            OracleParameter rasaIdParam = new OracleParameter("p_rasa_zvirat_id_rasa", OracleDbType.Int32, ParameterDirection.Input);
            rasaIdParam.Value = aktualni.RasaZviratIdRasa;

            DatabaseController.Execute("pkg_ostatni.upsert_majitel", zvireIdParam, jmenoParam, pohlaviParam, narozeniParam, umrtiParam, majitelZvireteIdParam, rasaIdParam);
        }
        public static int UpsertZvirePacient(int IdMajitel, JsonElement data)
        {
            Rasa aktualni = RasaZviratController.GetByJmenoRasa(data.GetProperty("rasa").GetString());
            if (aktualni.Id.Equals(null)) { aktualni.JmenoRasa = data.GetProperty("rasa").GetString(); aktualni.Id = -1; RasaZviratController.InsertRasa(aktualni); }

            OracleParameter zvireIdParam = new OracleParameter("p_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            zvireIdParam.Value = -1;

            OracleParameter jmenoParam = new OracleParameter("p_jmeno", OracleDbType.Varchar2, ParameterDirection.Input);
            jmenoParam.Value = data.GetProperty("jmenoZvire").GetString();

            OracleParameter pohlaviParam = new OracleParameter("p_pohlavi", OracleDbType.Varchar2, ParameterDirection.Input);
            pohlaviParam.Value = data.GetProperty("pohlavi").GetString();

            OracleParameter narozeniParam = new OracleParameter("p_datum_narozeni", OracleDbType.Date, ParameterDirection.Input);
            narozeniParam.Value = data.GetProperty("datumN").GetString();

            OracleParameter umrtiParam = new OracleParameter("p_datum_umrti", OracleDbType.Date, ParameterDirection.Input);
            umrtiParam.Value = data.GetProperty("datumU").GetString() ?? (object)DBNull.Value;

            OracleParameter majitelZvireteIdParam = new OracleParameter("p_majitel_zvire_id_pacient", OracleDbType.Int32, ParameterDirection.Input);
            majitelZvireteIdParam.Value = IdMajitel;

            OracleParameter rasaIdParam = new OracleParameter("p_rasa_zvirat_id_rasa", OracleDbType.Int32, ParameterDirection.Input);
            rasaIdParam.Value = aktualni.Id;

            DatabaseController.Execute("pkg_ostatni.upsert_majitel", zvireIdParam, jmenoParam, pohlaviParam, narozeniParam, umrtiParam, majitelZvireteIdParam, rasaIdParam);
            return int.Parse((zvireIdParam.Value).ToString());
        }

        public static void DeleteMapping(int zvireId)
        {
            DatabaseController.Execute("pkg_delete.delete_zvire_ma_nemoc_by_animal_name",
                new OracleParameter("p_jmeno_zvire", zvireId)
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
        public static List<Zvire> GetAll()
        {
            List<Zvire> Zvirata = new List<Zvire>();
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");

            if (query.Rows.Count == 0)
            {
                return null;
            }

            foreach (DataRow row in query.Rows)
            {
                Zvire zvire = new Zvire
                {
                    JmenoZvire = row[JMENO_ZVIRE_NAME].ToString(),
                    Pohlavi = row[POHLAVI_NAME].ToString(),
                    DatumNarozeni = DateTime.Parse(row[DATUM_NAROZENI_NAME].ToString()),
                    DatumUmrti = row[DATUM_UMRTI_NAME] == DBNull.Value ? null : (DateTime?)DateTime.Parse(row[DATUM_UMRTI_NAME].ToString()),
                    MajitelZvireIdPacient = int.Parse(row[MAJITEL_ZVIRE_ID_PACIENT_NAME].ToString()),
                    Id = int.Parse(row[ID_ZVIRE_NAME].ToString()),
                    RasaZviratIdRasa = int.Parse(row[RASA_ZVIRAT_ID_RASA_NAME].ToString())
                };
                Zvirata.Add(zvire);
            };
            return Zvirata;
        }
        public static void DeleteZvire(int id)
        {
            DatabaseController.Execute1($"DELETE FROM {TABLE_NAME} WHERE {ID_ZVIRE_NAME} = :id",
                new OracleParameter("id", id)
            );
        }
    }
}


