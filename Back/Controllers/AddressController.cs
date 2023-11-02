using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Runtime.Caching;
using System.Web.Http;
using TISBackend.Auth;
using TISBackend.Db;
using TISModelLibrary;

namespace Back.Controllers
{
    public class AddressController 
    {
        public const string TABLE_NAME = "ADRESY";
        public const string ID_NAME = "id_adresa";

       

        private static readonly AddressController instance = new AddressController();

       
        public static Address New(DataRow dr, AuthLevel authLevel, string idName = AddressController.ID_NAME)
        {
            return new Address()
            {
                Id = int.Parse(dr[idName].ToString()),
                City = (dr["mesto"].ToString() == "") ? null : dr["mesto"].ToString(),
                Street = (dr["ulice"].ToString() == "") ? null : dr["ulice"].ToString(),
                HouseNumber = (dr["cislo_popisne"].ToString() == "") ? null : (int?)int.Parse(dr["cislo_popisne"].ToString()),
            };
        }

        public IEnumerable<int> GetIds()
        {
            return GetIds(TABLE_NAME, ID_NAME);
        }

        // GET: api/Address
        public IEnumerable<Address> Get()
        {
            List<Address> list = new List<Address>();

                DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
                foreach (DataRow dr in query.Rows)
                {
                    list.Add(New(dr, GetAuthLevel()));
                }
            

            return list;
        }

        // GET: api/Address/5
        public Address Get(int id)
        {
                return null;
            

           

            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_NAME} = :id", new OracleParameter("id", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            Address address = New(query.Rows[0], GetAuthLevel());
            cachedAddresses.Add(id.ToString(), address, DateTimeOffset.Now.AddMinutes(15));
            return address;
        }

        protected override bool CheckObject(JObject value, AuthLevel authLevel)
        {
            return ValidJSON(value, "Id") && int.TryParse(value["Id"].ToString(), out _);
        }

       
        protected override int SetObjectInternal(JObject value, AuthLevel authLevel, OracleTransaction transaction)
        {
            Address n = value.ToObject<Address>();
            OracleParameter p_id = new OracleParameter("p_id", n.Id);
            DatabaseController.Execute("PKG_MODEL_DML.UPSERT_ADRESA", transaction,

                p_id,
                new OracleParameter("p_mesto", n.City),
                new OracleParameter("p_ulice", n.Street),
                new OracleParameter("p_cp", n.HouseNumber)
              
               
            );
            int id = int.Parse(p_id.Value.ToString());

          

            return id;
        }

    
        public static bool CheckObjectStatic(JObject value, AuthLevel authLevel)
        {
            return instance.CheckObject(value, authLevel);
        }

  
        public static int SetObjectStatic(JObject value, AuthLevel authLevel, OracleTransaction transaction = null)
        {
            return instance.SetObject(value, authLevel, transaction);
        }

       
    }
}
