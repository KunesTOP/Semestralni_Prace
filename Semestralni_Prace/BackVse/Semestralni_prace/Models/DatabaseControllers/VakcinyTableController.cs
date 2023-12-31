﻿using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace Models.DatabaseControllers
{
    public class VakcinyTableController 
    {
        public const string TABLE_NAME = "VAKCINY";
        public const string ID_VAKCINA = "id_vakcina";
        public const string NAZEV_VAKCINA_NAME = "nazev_vakcina";
        public const string VIEW_NAME = "ASISTENT_PODA_VAKCINU_ZVIRETI";

        public static IEnumerable<int> GetVakcinaIds()
        {
            return GetIds(TABLE_NAME, ID_VAKCINA);
        }

        public static Vakcina GetByNazevVakcina(string nazevVakcina)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {NAZEV_VAKCINA_NAME} = :nazevVakcina",
                new OracleParameter("nazevVakcina", nazevVakcina));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Vakcina()
            {
                Id = int.Parse(query.Rows[0][ID_VAKCINA].ToString()),
                NazevVakcina = query.Rows[0][NAZEV_VAKCINA_NAME].ToString()
            };
        }

        
        public static void UpsertVakcina(int id, JsonElement data)
        {
            OracleParameter idVakcinaParam = new OracleParameter("idVakcina", OracleDbType.Int32, id, ParameterDirection.Input);
            OracleParameter nazevVakcinaParam = new OracleParameter("nazevVakcina", OracleDbType.Varchar2, data.GetProperty("nazevVakcina").GetString(), ParameterDirection.Input);

            DatabaseController.Execute(
               "pkg_ostatni.upsert_vakciny",
                idVakcinaParam,
                nazevVakcinaParam
            );
        }
        public static void DeleteVakcina(int idVakcina)
        {
            OracleParameter idVakcinaParam = new OracleParameter("idVakcina", OracleDbType.Int32, idVakcina, ParameterDirection.Input);

            DatabaseController.Execute(
               "pkg_ostatni.delete_vakcina",
                idVakcinaParam
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

        public static List<Vakcina> GetAll()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            List<Vakcina> listVakcin = new List<Vakcina>();
            if (query.Rows.Count == 0)
            {
                return null;
            }
            try
            {     
                foreach (DataRow dr in query.Rows)
                {
                    listVakcin.Add(new Vakcina()
                    {
                        Id = int.Parse(dr[ID_VAKCINA].ToString()),
                        NazevVakcina = dr[NAZEV_VAKCINA_NAME].ToString()
                    });
                }

            }catch(Exception ex)
            {

            }
            return listVakcin;
        }
        public static List<VakcinaceZvirat> GetAsistentPodaVakcinuZviretiView() {
            DataTable query = DatabaseController.Query($"SELECT * FROM {VIEW_NAME}");
            if(query.Rows.Count == 0)
            {
                return null;
            }
            List<VakcinaceZvirat> listVakcinaci = new List<VakcinaceZvirat>();
            foreach(DataRow dr in query.Rows)
            {
                listVakcinaci.Add(new VakcinaceZvirat
                {
                    Krestni = dr["ZAMESTNANEC_KRESTNI"].ToString(),
                    Prijmeni = dr["ZAMESTNANEC_PRIJMENI"].ToString(),
                    Profese = dr["ZAMESTNANEC_PROFESE"].ToString(),
                    VakcinaNazev = dr["VAKCINA_NAZEV"].ToString(),
                    ZvireJmeno = dr["ZVIRE_JMENO"].ToString(),
                    ZvirePohlavi = dr["ZVIRE_POHLAVI"].ToString(),
                    ZvireDatumNarozeni = DateTime.Parse(dr["ZVIRE_DATUM_NAROZENI"].ToString()),
                    ZvireDatumUmrti = dr["ZVIRE_DATUM_UMRTI"] == DBNull.Value ? null : (DateTime?)DateTime.Parse(dr["ZVIRE_DATUM_UMRTI"].ToString()),
                }) ;


            }

            return listVakcinaci;
        }
    }
}


