using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace Models.DatabaseControllers
{
    public class LekariController
    {
        public const string TABLE_NAME = "LEKARI";
        public const string ID_NAME = "id_zamestnanec";
        public const string AKREDITACE_NAME = "akreditace";

        public static IEnumerable<int> GetIds()
        {
            return GetIds(TABLE_NAME, ID_NAME);
        }
        public static void DeleteLekar(int id)
        {
            DatabaseController.Execute1($"DELETE FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id)
            );
        }

        public static void UpsertLekar(int id, JsonElement data)
        {
            OracleParameter idParam = new OracleParameter("p_id_zamestnanec", OracleDbType.Int32, ParameterDirection.InputOutput);
            idParam.Value = id;

            OracleParameter akreditaceParam = new OracleParameter("p_akreditace", OracleDbType.Varchar2, ParameterDirection.Input);
            akreditaceParam.Value = data.GetProperty("akreditace").GetString();

            ZamestnanciController.UpsertZamestnanec(id, data);
            DatabaseController.Execute("pkg_model_dml1.upsert_lekar", idParam, akreditaceParam);
           
        }
        public static Lekar Get(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Lekar()
            {
                Id = int.Parse(query.Rows[0][ID_NAME].ToString()),
                Akreditace = query.Rows[0][AKREDITACE_NAME].ToString()
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
        public static IEnumerable<Lekar> GetAll()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            if (query.Rows.Count == 0)
            {

                return null;
            }
            List<Lekar> listLekaru = new List<Lekar>();
            List<Zamestnanec> listZamestnancu = ZamestnanciController.GetAll();
            foreach (DataRow dr in query.Rows)
            {
                Zamestnanec zamestnanec = listZamestnancu.FirstOrDefault(z => z.Id == int.Parse(dr[ID_NAME].ToString()));
                listLekaru.Add(new Lekar
                {
                    Id = int.Parse(dr[ID_NAME].ToString()),
                    Jmeno = zamestnanec.Jmeno,
                    Prijmeni = zamestnanec.Prijmeni,
                    Profese = zamestnanec.Profese,
                    VeterKlinId = zamestnanec.VeterKlinId,
                    Akreditace = dr[AKREDITACE_NAME].ToString()
                });
            }
            return listLekaru;
        }
    }
}


