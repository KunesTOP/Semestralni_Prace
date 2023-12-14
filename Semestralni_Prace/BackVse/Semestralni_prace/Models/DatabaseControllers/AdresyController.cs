using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using System.Net;
using Back.Auth;

using System.Data;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Runtime.Caching;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Semestralni_prace.Models.Classes;
using System.Text.Json;

namespace Models.DatabaseControllers
{
    public class AdresyController 
    {
        public const string TABLE_NAME = "ADRESY";
        public const string ID_NAME = "id_adresa";
        public const string MESTO_NAME = "mesto";
        public const string ULICE_NAME = "ulice";
        public const string CISLO_POPISNE_NAME = "cislo_popisne";


        protected static ObjectCache cachedAddresses = MemoryCache.Default;


        public static Adresy New(DataRow dr, AuthLevel authLevel, string idName = AdresyController.ID_NAME)
        {
            return new Adresy()
            {
                Id = int.Parse(dr[idName].ToString()),
                City = (dr["mesto"].ToString() == "") ? null : dr["mesto"].ToString(),
                Street = (dr["ulice"].ToString() == "") ? null : dr["ulice"].ToString(),
                HouseNumber = (dr["cislo_popisne"].ToString() == "") ? null : (int?)int.Parse(dr["cislo_popisne"].ToString()),
            };
        }

        public IEnumerable<int> GetIds(string tABLE_NAME, string iD_NAME)
        {
            return GetIds(TABLE_NAME, ID_NAME);// TODO
        }


        public IEnumerable<Adresy> Get()
        {
            List<Adresy> list = new List<Adresy>();

            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            foreach (DataRow dr in query.Rows)
            {
                list.Add(New(dr, GetAuthLevel()));//TODO
            }


            return list;
        }
        public static int GetIdByCity(string city)
        {
            DataTable query = DatabaseController.Query($"SELECT {ID_NAME} FROM {TABLE_NAME} WHERE {MESTO_NAME} = :city",
                new OracleParameter("city", city));
            if(query.Rows.Count != 1) { return 0; }


            return int.Parse(query.Rows[0][ID_NAME].ToString());
        }

        private static AuthLevel GetAuthLevel()
        {
            throw new NotImplementedException();
        }

        public static Adresy Get(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_NAME} = :id", new OracleParameter("id", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            Adresy address = New(query.Rows[0], GetAuthLevel());// TODO
            cachedAddresses.Add(id.ToString(), address, DateTimeOffset.Now.AddMinutes(15));//TODO
            return address;
        }

        public static void Upsert(int id, JsonElement data)
        {
            OracleParameter idParam = new OracleParameter("p_id_adresa", OracleDbType.Int32, ParameterDirection.InputOutput);
            idParam.Value = id;

            OracleParameter mestoParam = new OracleParameter("p_mesto", OracleDbType.Varchar2, ParameterDirection.InputOutput);
            mestoParam.Value = data.GetProperty("city").GetString();

            OracleParameter uliceParam = new OracleParameter("p_ulice", OracleDbType.Varchar2, ParameterDirection.InputOutput);
            uliceParam.Value = data.GetProperty("street").GetString();

            int cisloPopisne = int.Parse(data.GetProperty("houseNumber").GetString());
            OracleParameter cisloPopisneParam = new OracleParameter("p_cislo_popisne", OracleDbType.Int32, ParameterDirection.InputOutput);
            cisloPopisneParam.Value = cisloPopisne;

            DatabaseController.Execute("pkg_model_dml1.insert_adresa", idParam, mestoParam, uliceParam, cisloPopisneParam);
        }

        public static void Delete(int id)
        {
            DatabaseController.Execute($"DELETE FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id)
            );
        }
        
        public static IEnumerable<Adresy> GetAll()
        {
            List<Adresy> list = new List<Adresy>();

            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            if (query.Rows.Count == 0)
            {
                return null;
            }

            foreach (DataRow dr in query.Rows)
            {
                list.Add(new Adresy
                {
                    Id = int.Parse(dr[ID_NAME].ToString()),
                    City = dr[MESTO_NAME].ToString(),
                    Street = dr[ULICE_NAME].ToString(),
                    HouseNumber = int.Parse(dr[CISLO_POPISNE_NAME].ToString())
                });
            }

            return list;
        }
    }
}
