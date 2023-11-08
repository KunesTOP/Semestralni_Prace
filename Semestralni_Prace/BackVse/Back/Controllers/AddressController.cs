using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using System.Net;
using Back.Auth;

using System.Data;
using System.Runtime.InteropServices;
using Semestralni_Práce.Classes;

using SemPrace.Shared.Semestralni_Prace.Semestralni_Prace.Back.Controllers;
using System.Security.Cryptography;
using System.Runtime.Caching;
using Newtonsoft.Json.Linq;


namespace Back.Controllers
{
    public class AddressController //: ControllerWithInt todo
    {
        public const string TABLE_NAME = "ADRESY";
        public const string ID_NAME = "id_adresa";

        protected static readonly ObjectCache cachedAddresses = MemoryCache.Default;


        private static readonly AddressController instance = new AddressController();


        public static Adress New(DataRow dr, AuthLevel authLevel, string idName = AddressController.ID_NAME)
        {
            return new Adress()
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


        public IEnumerable<Adress> Get()
        {
            List<Adress> list = new List<Adress>();

            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            foreach (DataRow dr in query.Rows)
            {
                list.Add(New(dr, GetAuthLevel()));//TODO
            }


            return list;
        }

        private AuthLevel GetAuthLevel()
        {
            throw new NotImplementedException();
        }

        public Adress Get(int id)
        {





            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_NAME} = :id", new OracleParameter("id", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            Adress address = New(query.Rows[0], GetAuthLevel());// TODO
            cachedAddresses.Add(id.ToString(), address, DateTimeOffset.Now.AddMinutes(15));//TODO
            return address;
        }

        //protected override bool CheckObject(JObject value, AuthLevel authLevel) //TODO
        //{
        //    return ValidJSON(value, "Id") && int.TryParse(value["Id"].ToString(), out _);//TODO
        //}

        private bool ValidJSON(JObject value, string v)
        {
            throw new NotImplementedException();
        }

        //protected override int SetObjectInternal(JObject value, AuthLevel authLevel, OracleTransaction transaction)//TODO
        //{
        //    Adress n = value.ToObject<Adress>();
        //    OracleParameter p_id = new OracleParameter("p_id", n.Id);
        //    DatabaseController.Execute("PKG_MODEL_DML.UPSERT_ADRESA", transaction,

        //        p_id,
        //        new OracleParameter("p_mesto", n.City),
        //        new OracleParameter("p_ulice", n.Street),
        //        new OracleParameter("p_cp", n.HouseNumber)


        //    );
        //    int id = int.Parse(p_id.Value.ToString());



        //    return id;
        //}


        //public static bool CheckObjectStatic(JObject value, AuthLevel authLevel)//TODO
        //{
        //    return instance.CheckObject(value, authLevel);
        //}


        //public static int SetObjectStatic(JObject value, AuthLevel authLevel, OracleTransaction transaction = null)//TODO
        //{
        //    return instance.SetObject(value, authLevel, transaction);//TODO
        //}


    }
}
