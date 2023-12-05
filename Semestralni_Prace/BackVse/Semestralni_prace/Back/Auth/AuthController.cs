using Back.databaze;
using Microsoft.Extensions.Caching.Memory;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Net.Http.Headers;
using System.Runtime.Caching;

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
        private static readonly ObjectCache cachedTokens = System.Runtime.Caching.MemoryCache.Default;
        private static AuthLevel CheckInDatabase(AuthToken authToken)
        {
            DataTable query = DatabaseController.Query(
                $"SELECT PKG_HESLA.ZJISTI_UROVEN(:jmeno, :hash) \"level\" FROM DUAL",
                new OracleParameter("jmeno", authToken.PrihlasovaciJmeno),
                new OracleParameter("hash", authToken.Hash)
            );
            return query.Rows.Count > 0 && Enum.TryParse((query.Rows[0])["level"].ToString(), out AuthLevel result) ? result : AuthLevel.NONE;
        }

        public static AuthLevel Check(AuthToken? authToken)
        {
            if (authToken == null)
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
            return  level;
        }

        public static void InvalidateCache(AuthToken? authToken)
        {
            if (authToken != null)
            {
                cachedTokens.Remove(authToken.Value.PrihlasovaciJmeno + authToken.Value.Hash);
            }
        }
    }
}
