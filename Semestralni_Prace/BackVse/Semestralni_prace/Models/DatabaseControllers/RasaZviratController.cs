﻿using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;

namespace Models.DatabaseControllers
{
    public class RasaZviratController : Controller//TODO knihovna
    {
        public const string TABLE_NAME = "RASA_ZVIRAT";
        public const string JMENO_RASA_NAME = "jmeno_rasa";
        public const string ID_RASA_NAME = "id_rasa";

        public static IEnumerable<int> GetRasaIds()
        {
            return GetIds(TABLE_NAME, ID_RASA_NAME);
        }

        public static Rasa GetByJmenoRasa(string jmenoRasa)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {JMENO_RASA_NAME} = :jmenoRasa",
                new OracleParameter("jmenoRasa", jmenoRasa));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Rasa()
            {
                JmenoRasa = query.Rows[0][JMENO_RASA_NAME].ToString(),
                IdRasa = int.Parse(query.Rows[0][ID_RASA_NAME].ToString())
            };
        }

        public static void InsertRasa(Rasa rasa)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({JMENO_RASA_NAME}, {ID_RASA_NAME}) " +
                $"VALUES (:jmenoRasa, :idRasa)",
                new OracleParameter("jmenoRasa", rasa.JmenoRasa),
                new OracleParameter("idRasa", rasa.IdRasa)
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

        public static Rasa GetById(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_RASA_NAME} = :idRasa",
                new OracleParameter("idRasa", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Rasa()
            {
                JmenoRasa = query.Rows[0][JMENO_RASA_NAME].ToString(),
                IdRasa = int.Parse(query.Rows[0][ID_RASA_NAME].ToString())
            };
        }
        public static IEnumerable<Rasa> GetAll()
        {
            List<Rasa> listRas = new List<Rasa>();

            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");

            foreach (DataRow dr in query.Rows)
            {
                listRas.Add(new Rasa
                {
                    IdRasa = int.Parse(dr[ID_RASA_NAME].ToString()),
                    JmenoRasa = dr[JMENO_RASA_NAME].ToString()
                });
            }

            return listRas;
        }
    }
}


