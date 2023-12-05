using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Data;

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

        public static void InsertUcty(Ucty ucty)
        {
            DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({JMENO_NAME}, {SALT_NAME}, {HASH_NAME}, {UROVEN_NAME}) VALUES (:jmeno, :salt, :hash, :uroven)",
                new OracleParameter("jmeno", ucty.Jmeno),
                new OracleParameter("salt", ucty.Salt),
                new OracleParameter("hash", ucty.Hash),
                new OracleParameter("uroven", ucty.Uroven)
            );
        }

        public static void UpdateUcty(Ucty ucty)
        {
            DatabaseController.Execute($"UPDATE {TABLE_NAME} SET {JMENO_NAME} = :jmeno, {SALT_NAME} = :salt, {HASH_NAME} = :hash, {UROVEN_NAME} = :uroven WHERE {ID_NAME} = :id",
                new OracleParameter("id", ucty.IdClovek),
                new OracleParameter("jmeno", ucty.Jmeno),
                new OracleParameter("salt", ucty.Salt),
                new OracleParameter("hash", ucty.Hash),
                new OracleParameter("uroven", ucty.Uroven)
            );
        }

        public static void DeleteUcty(int id)
        {
            DatabaseController.Execute($"DELETE FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id));
        }
    }
}
