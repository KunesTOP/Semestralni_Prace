using Back.Auth;
using Back.databaze;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SemPrace.Shared.Semestralni_Prace.Semestralni_Prace.Back.Controllers
{
    internal abstract class Controller<TId> : ApiController
    {
        protected abstract TId ErrId { get; }

        [NonAction]
        protected AuthLevel GetAuthLevel()
        {
            return AuthController.Check(AuthToken.From(Request.Headers));
        }

        [NonAction]
        protected bool IsAdmin()
        {
            return GetAuthLevel() == AuthLevel.ADMIN;
        }

        [NonAction]
        protected bool IsAuthorized()
        {
            return GetAuthLevel() != AuthLevel.NONE;
        }

        [NonAction]
        protected bool HasHigherAuth()
        {
            AuthLevel authLevel = GetAuthLevel();
            return authLevel == AuthLevel.ADMIN || authLevel == AuthLevel.INNER;
        }

        [NonAction]
        protected bool ValidJSON(JObject value, params string[] keys)
        {
            if (value == null)
            {
                return false;
            }
            foreach (string key in keys)
            {
                if (!value.ContainsKey(key))
                {
                    return false;
                }
            }
            return true;
        }

        [NonAction]
        protected abstract List<TId> GetIds(string tableName, string idName, bool allowUnauthorized = false);




        protected virtual bool CheckObject(JObject value, AuthLevel authLevel) { return authLevel != AuthLevel.NONE; }


        protected virtual TId SetObjectInternal(JObject value, AuthLevel authLevel, OracleTransaction transaction) { return default; }


        protected TId SetObject(JObject value, AuthLevel authLevel, OracleTransaction transaction = null)
        {
            bool newTransaction = transaction == null;
            transaction = transaction ?? DatabaseController.StartTransaction();

            TId res = ErrId;
            try
            {
                res = SetObjectInternal(value, authLevel, transaction);
            }
            catch (Exception)
            {
                
                if (newTransaction) DatabaseController.Rollback(transaction);

                throw;

            }

            if (newTransaction)
            {
                if (res.Equals(ErrId))
                {
                    DatabaseController.Rollback(transaction);
                }
                else
                {
                    DatabaseController.Commit(transaction);
                }
            }

            return res;
        }

        public class ControllerWithInt : Controller<int>
        {
            protected override int ErrId => throw new NotImplementedException();

            protected override List<int> GetIds(string tableName, string idName, bool allowUnauthorized = false)
            {
                throw new NotImplementedException();
            }
        }





    }
}

