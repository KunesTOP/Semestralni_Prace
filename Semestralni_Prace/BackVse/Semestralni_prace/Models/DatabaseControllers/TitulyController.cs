using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace Models.DatabaseControllers
{
    public class TitulyController : Controller//TODO KNIHOVNA
    {
        public const string TABLE_NAME = "TITULY";
        public const string ID_TITUL_NAME = "id_titul";
        public const string ZKRATKA_TITUL_NAME = "zkratka_titul";
        public const string NAZEV_TITUL_NAME = "nazev_titul";

        public static IEnumerable<int> GetTitulIds()
        {
            return GetIds(TABLE_NAME, ID_TITUL_NAME);
        }

        public static Titul GetByZkratkaTitul(string zkratkaTitul)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ZKRATKA_TITUL_NAME} = :zkratkaTitul",
                new OracleParameter("zkratkaTitul", zkratkaTitul));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Titul()
            {
                Id = int.Parse(query.Rows[0][ID_TITUL_NAME].ToString()),
                ZkratkaTitul = query.Rows[0][ZKRATKA_TITUL_NAME].ToString(),
                NazevTitul = query.Rows[0][NAZEV_TITUL_NAME].ToString()
            };
        }
        public static Titul GetByTitulId(int idTitul)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_TITUL_NAME} = :idTitul",
                new OracleParameter("idTitul", idTitul));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Titul()
            {
                Id = int.Parse(query.Rows[0][ID_TITUL_NAME].ToString()),
                ZkratkaTitul = query.Rows[0][ZKRATKA_TITUL_NAME].ToString(),
                NazevTitul = query.Rows[0][NAZEV_TITUL_NAME].ToString()
            };
        }

        public static void InsertTitul(Titul titul)
        {
            Titul existingTitul = GetByTitulId(titul.Id);

            if (existingTitul == null)
            {

                DatabaseController.Execute1($"INSERT INTO {TABLE_NAME} ({ID_TITUL_NAME}, {NAZEV_TITUL_NAME}, {ZKRATKA_TITUL_NAME}) " +
                    $"VALUES (:idTitul, :nazevTitul, :zkratkaTitul)",
                    new OracleParameter("idTitul", titul.Id),
                    new OracleParameter("nazevTitul", titul.NazevTitul),
                    new OracleParameter("zkratkaTitul", titul.ZkratkaTitul)
                );
            }
            else

            {
                DeleteTitul(titul.Id);
                DatabaseController.Execute1($"INSERT INTO {TABLE_NAME} ({ID_TITUL_NAME}, {NAZEV_TITUL_NAME}, {ZKRATKA_TITUL_NAME}) " +
            $"VALUES (:idTitul, :nazevTitul, :zkratkaTitul)",
            new OracleParameter("idTitul", titul.Id),
            new OracleParameter("nazevTitul", titul.NazevTitul),
            new OracleParameter("zkratkaTitul", titul.ZkratkaTitul));
            }
        }

        public static void DeleteTitul(int idTitul)
        {
            DatabaseController.Execute1($"DELETE FROM {TABLE_NAME} WHERE {ID_TITUL_NAME} = :idTitul",
                new OracleParameter("idTitul", idTitul)
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

        public static void UpsertTitul(int id, JsonElement data)
        {
            OracleParameter idTitulParam = new OracleParameter("p_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            idTitulParam.Value = id;

            OracleParameter zkratkaParam = new OracleParameter("p_zkratka", OracleDbType.Varchar2, ParameterDirection.Input);
            zkratkaParam.Value = data.GetProperty("zkratkaTitul").GetString();

            OracleParameter nazevParam = new OracleParameter("p_nazev", OracleDbType.Varchar2, ParameterDirection.Input);
            nazevParam.Value = data.GetProperty("nazevTitul").GetString();

            DatabaseController.Execute("pkg_model_dml1.upsert_tituly", idTitulParam, zkratkaParam,nazevParam);
        }

        public static IEnumerable<Titul> GetAll()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            List<Titul> listTituly = new List<Titul>();

            if (query.Rows.Count == 0)
            {
                return null;
            }
            foreach (DataRow dr in query.Rows)
            {
                listTituly.Add(new Titul()
                {
                    Id = int.Parse(dr[ID_TITUL_NAME].ToString()),
                    ZkratkaTitul = dr[ZKRATKA_TITUL_NAME].ToString(),
                    NazevTitul = dr[NAZEV_TITUL_NAME].ToString()
                });

            }
            return listTituly;
        }
    }
}


