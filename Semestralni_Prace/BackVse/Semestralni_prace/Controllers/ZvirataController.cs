using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_Práce.Classes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Back.Controllers
{
    public class ZvirataController : Controller//TODO KNIHOVNA
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
                IdZvire = int.Parse(query.Rows[0][ID_ZVIRE_NAME].ToString()),
                RasaZviratIdRasa = int.Parse(query.Rows[0][RASA_ZVIRAT_ID_RASA_NAME].ToString())
            };
        }

        public static void InsertZvire(Zvire zvire)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} " +
                $"({JMENO_ZVIRE_NAME}, {POHLAVI_NAME}, {DATUM_NAROZENI_NAME}, {DATUM_UMRTI_NAME}, {MAJITEL_ZVIRE_ID_PACIENT_NAME}, {ID_ZVIRE_NAME}, {RASA_ZVIRAT_ID_RASA_NAME}) " +
                $"VALUES (:jmenoZvire, :pohlavi, :datumNarozeni, :datumUmrti, :majitelZvireIdPacient, :idZvire, :rasaZviratIdRasa)",
                new OracleParameter("jmenoZvire", zvire.JmenoZvire),
                new OracleParameter("pohlavi", zvire.Pohlavi),
                new OracleParameter("datumNarozeni", zvire.DatumNarozeni),
                new OracleParameter("datumUmrti", zvire.DatumUmrti ?? (object)DBNull.Value),
                new OracleParameter("majitelZvireIdPacient", zvire.MajitelZvireIdPacient),
                new OracleParameter("idZvire", zvire.IdZvire),
                new OracleParameter("rasaZviratIdRasa", zvire.RasaZviratIdRasa)
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


