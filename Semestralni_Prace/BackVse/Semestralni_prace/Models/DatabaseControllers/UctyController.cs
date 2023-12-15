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
        public const string ID_COLUMN_NAME = "id";
        public const string JMENO_COLUMN_NAME = "jmeno";
        public const string HESLO_HASH_COLUMN_NAME = "heslo_hash";
        public const string UROVEN_AUTORIZACE_COLUMN_NAME = "uroven_autorizace";


        public static void CreateUcty(Ucty ucty)
        {
            ucty.Hash = PasswordHelper.HashPassword(ucty.Hash);
            CreateUctyBezHashe(ucty);
        }
        public static void CreateUctyBezHashe(Ucty ucty)
        {
            DatabaseController.Execute1(
                $"INSERT INTO {TABLE_NAME} (jmeno, heslo_hash, uroven_autorizace) VALUES (:jmeno, :heslo_hash, :uroven_autorizace)",
                new OracleParameter("jmeno", ucty.Jmeno),
                new OracleParameter("heslo_hash", ucty.Hash),
                new OracleParameter("uroven_autorizace", ucty.Uroven));
        }


        // Read an account by ID
        public static Ucty GetUctyById(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE id = :id",
                new OracleParameter("id", id));
            if (query.Rows.Count > 0)
            {
                DataRow dr = query.Rows[0];
                return new Ucty
                {
                    Id = Convert.ToInt32(dr["id"]),
                    Jmeno = dr["jmeno"].ToString(),
                    Hash = dr["heslo_hash"].ToString(),
                    Uroven = Convert.ToInt32(dr["uroven_autorizace"])
                };
            }
            return null;
        }

        // Update an existing account
        public static void UpdateUcty(int id, JsonElement data)
        {
            string hesloHash = PasswordHelper.HashPassword(data.GetProperty("heslo_hash").GetString());
            string jmeno = data.GetProperty("jmeno").GetString();

            int urovenAutorizace = data.GetProperty("uroven_autorizace").GetInt32();

            DatabaseController.Execute1(
                $"UPDATE ucty SET jmeno = :jmeno, heslo_hash = :heslo_hash, uroven_autorizace = :uroven_autorizace WHERE id = :id",
                new OracleParameter("id", id),
                new OracleParameter("jmeno", jmeno),
                new OracleParameter("heslo_hash", hesloHash),
                new OracleParameter("uroven_autorizace", urovenAutorizace));
        }





        // Delete an account
        public static void DeleteUcty(int id)
        {
            DatabaseController.Execute1($"DELETE FROM {TABLE_NAME} WHERE id = :id",
                new OracleParameter("id", id));
        }

        // Get all accounts
        public static List<Ucty> GetAllUcty()
        {
            List<Ucty> uctyList = new List<Ucty>();
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            foreach (DataRow dr in query.Rows)
            {
                uctyList.Add(new Ucty
                {
                    Id = Convert.ToInt32(dr["id"]),
                    Jmeno = dr["jmeno"].ToString(),
                    Hash = dr["heslo_hash"].ToString(),
                    Uroven = Convert.ToInt32(dr["uroven_autorizace"])
                });
            }
            return uctyList;
        }
    }
}

