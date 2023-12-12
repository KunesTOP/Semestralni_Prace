using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Runtime.Caching;
using System.Text;
using System.Security.Cryptography;


namespace Back.Auth

{
    public struct AuthToken
    {
        public string PrihlasovaciJmeno { get; set; }
        public string Hash { get; set; }

    }
    public enum AuthLevel
    {
        NONE = -1, OUTER = 2, INNER = 1, ADMIN = 0
    }

    internal class AuthController

    {
        public const string TABLE_NAME = "UCTY";
        //Na todle nešahej, funguje to! Pokud si todle čte někdo jiný, než Jindra... o> jak je?
        private static readonly ObjectCache cachedTokens = System.Runtime.Caching.MemoryCache.Default;
        public static AuthLevel CheckInDatabase(AuthToken authToken)
        {
            DataTable query = DatabaseController.Query(
                $"SELECT heslo_hash FROM UCTY WHERE jmeno = :jmeno",
                new OracleParameter("jmeno", authToken.PrihlasovaciJmeno)
            );

            if (query.Rows.Count == 1)
            {
                string dbHash = query.Rows[0]["heslo_hash"].ToString();
                Console.WriteLine(authToken.Hash + dbHash);
                // Porovnejte hash z databáze s hashem z AuthToken
                if (VerifyPassword(authToken.Hash, dbHash))
                {
                    DataTable authLevelQuery = DatabaseController.Query(
                         $"SELECT uroven_autorizace FROM {TABLE_NAME} WHERE jmeno = :jmeno",
                        new OracleParameter("jmeno", authToken.PrihlasovaciJmeno)
                        
                    );

                    if (authLevelQuery.Rows.Count > 0 && Enum.TryParse(authLevelQuery.Rows[0]["Uroven_Autorizace"].ToString(), out AuthLevel result))
                    {
                        return result;

                    }
                }
            }

            return AuthLevel.NONE;
        }

        // Metoda pro ověření hesla pomocí hashů
        private static bool VerifyPassword(string inputPassword, string hashedPasswordFromDatabase)
        {
            // Zde byste měli implementovat logiku pro porovnání hashů hesel
            // Použijte například HMACSHA256 pro ověření hesla

            // Příklad:
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(inputPassword)))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));

                string computedHashString = BitConverter.ToString(computedHash).Replace("-", "").ToLower();

                return computedHashString == hashedPasswordFromDatabase;
            }
        }



        public static AuthLevel Check(AuthToken? authToken)
        {
            if (!authToken.HasValue)
            {
                return AuthLevel.NONE;
            }

            AuthToken token = authToken.Value;

            AuthLevel? cachedLevel = cachedTokens[token.PrihlasovaciJmeno] as AuthLevel?;
            if (cachedLevel != null)
            {
                return cachedLevel.Value;
            }

            AuthLevel level = CheckInDatabase(token);
            cachedTokens.Add(token.PrihlasovaciJmeno + token.Hash, level, DateTimeOffset.Now.AddMinutes(15));
            return level;
        }

        public static void InvalidateCache(AuthToken? authToken)
        {
            if (authToken != null)
            {
                cachedTokens.Remove(authToken.Value.PrihlasovaciJmeno + authToken.Value.Hash);
            }
        }
        public static AuthLevel GetLevel(string jmeno)
        {
            DataTable query = DatabaseController.Query($"SELECT uroven FROM {"UCTY"} WHERE {{JMENO}} = :jmeno",
                new OracleParameter("jmeno", jmeno));

            if (query.Rows.Count != 1)
            {
                return AuthLevel.NONE;
            }
            return Enum.TryParse((query.Rows[0])["UROVEN"].ToString(), out AuthLevel result) ? result : AuthLevel.NONE;
        }


    }
}
