using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace Models.DatabaseControllers
{
    public class LekyController 
    {
        public const string TABLE_NAME = "LEKY";
        public const string ID_NAME = "id_leky";
        public const string NAZEV_NAME = "nazev";

        public static IEnumerable<int> GetIds()
        {
            return GetIds(TABLE_NAME, ID_NAME);
        }

        public static Lek Get(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Lek()
            {
                Id = int.Parse(query.Rows[0][ID_NAME].ToString()),
                Nazev = query.Rows[0][NAZEV_NAME].ToString()
            };
        }

       

        public static void DeleteLek(int id)
        {
            DatabaseController.Execute($"DELETE FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id)
            );
        }

        public static void UpsertLek(int id, JsonElement data)
        {
            OracleParameter idParam = new OracleParameter("p_id_leky", OracleDbType.Int32, ParameterDirection.InputOutput);
            idParam.Value = id;

            OracleParameter nazevParam = new OracleParameter("p_nazev", OracleDbType.Varchar2, ParameterDirection.Input);
            nazevParam.Value = data.GetProperty("nazev").GetString();
            var debugg = data.GetProperty("nazev").GetString();
            DatabaseController.Execute("pkg_ostatni.upsert_lek", idParam, nazevParam);
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
        public static List<Lek> GetAll()
        {
            List<Lek> listLeky = new List<Lek>();
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");

            if (query.Rows.Count == 0)
            {
                return null;
            }

            foreach (DataRow row in query.Rows)
            {
                listLeky.Add(new Lek
                {
                    Id = int.Parse(row[ID_NAME].ToString()),
                    Nazev = row[NAZEV_NAME].ToString()
                });
            };
            return listLeky;
        }


    }
}


