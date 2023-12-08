using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Data;
using System.Text.Json;

namespace Semestralni_prace.Models.DatabaseControllers
{
    public class UctyController
    {
        public const string TABLE_NAME = "UCTY";
        public const string ID_NAME = "id_clovek";
        public const string JMENO_NAME = "jmeno";
        public const string SALT_NAME = "salt";
        public const string HASH_NAME = "hash";
        public const string UROVEN_NAME = "uroven";

        public static Ucty GetUctyById(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id));

            if (query.Rows.Count > 0)
            {
                DataRow dr = query.Rows[0];
                return new Ucty
                {
                    IdClovek = Convert.ToInt32(dr[ID_NAME]),
                    Jmeno = dr[JMENO_NAME].ToString(),
                    Salt = dr[SALT_NAME].ToString(),
                    Hash = dr[HASH_NAME].ToString(),
                    Uroven = Convert.ToInt32(dr[UROVEN_NAME])
                };
            }
            else
            {
                return null;
            }
        }

        public static Ucty GetUctyByJmeno(string jmeno)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {JMENO_NAME} = :jmeno",
                new OracleParameter("jmeno", jmeno));

            if (query.Rows.Count > 0)
            {
                DataRow dr = query.Rows[0];
                return new Ucty
                {
                    IdClovek = Convert.ToInt32(dr[ID_NAME]),
                    Jmeno = dr[JMENO_NAME].ToString(),
                    Salt = dr[SALT_NAME].ToString(),
                    Hash = dr[HASH_NAME].ToString(),
                    Uroven = Convert.ToInt32(dr[UROVEN_NAME])
                };
            }
            else
            {
                return null;
            }
        }

        public static void UpsertUcty(int id, JsonElement data)
        {
            OracleParameter jmeno = new OracleParameter("jmeno", OracleDbType.Varchar2, ParameterDirection.InputOutput);
            jmeno.Value = data.GetProperty("jmeno").GetString();

            OracleParameter hash = new OracleParameter("hash", OracleDbType.Varchar2, ParameterDirection.InputOutput);
            hash.Value = data.GetProperty("hash").GetString();

            OracleParameter uroven = new OracleParameter("uroven", OracleDbType.Int32, ParameterDirection.InputOutput);
            uroven.Value = data.GetProperty("uroven").GetString();

            DatabaseController.Execute("pkg_hesla.nastav_ucet", jmeno, hash, uroven);
        }

        public static void DeleteUcty(int id)
        {
            DatabaseController.Execute($"DELETE FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id));
        }
        public static IEnumerable<Ucty> GetAll()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            if(query.Rows.Count== 0)
            {
                return null;
            }
            List<Ucty> listUctu = new List<Ucty>();

            foreach(DataRow dr in query.Rows)
            {
                listUctu.Add(new Ucty
                {
                    IdClovek = int.Parse(dr[ID_NAME].ToString()),
                    Jmeno = dr[JMENO_NAME].ToString(),
                    Salt = dr[SALT_NAME].ToString(),
                    Hash = dr[HASH_NAME].ToString(),
                    Uroven = int.Parse(dr[UROVEN_NAME].ToString())
                });
            }
            return listUctu;
          
        }
    }
}
