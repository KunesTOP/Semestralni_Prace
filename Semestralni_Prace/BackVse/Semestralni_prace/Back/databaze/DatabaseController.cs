using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Back.databaze
{
    class DatabaseController
    {
        private static string CONSTR =
            "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521)))(CONNECT_DATA=(SID=BDAS)));" +
            "user id=ST67057;password=abcde;" +
            "Connection Timeout=120;Validate connection=true;Min Pool Size=4;";
        private readonly static DatabaseController _databaseController = new DatabaseController();

        private readonly OracleConnection con = new OracleConnection(CONSTR);

        private static void OpenDb()
        {
            if (_databaseController.con.State != ConnectionState.Open)
            {
                _databaseController.con.Open();
            }
        }

        public static DataTable Query(string sql, params OracleParameter[] parameters)
        {
            OpenDb();
            DataTable dataTable = new DataTable();
            OracleCommand cmd = new OracleCommand(sql, _databaseController.con);
            foreach (OracleParameter op in parameters)
            {
                cmd.Parameters.Add(op);
            }

            OracleDataAdapter oda = new OracleDataAdapter(cmd);
            oda.Fill(dataTable);
            return dataTable;
        }
        public static void Query1(string sql, params OracleParameter[] parameters)
        {
            OpenDb();
            OracleCommand cmd = new OracleCommand(sql, _databaseController.con);
            foreach (OracleParameter op in parameters)
            {
                cmd.Parameters.Add(op);
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
        }

        public static DataTable Query(string sql, OracleTransaction transaction, params OracleParameter[] parameters)
        {
            OpenDb();
            DataTable dataTable = new DataTable();
            OracleCommand cmd = new OracleCommand(sql, _databaseController.con);
            if (transaction != null)
            {
                cmd.Transaction = transaction;
            }
            foreach (OracleParameter op in parameters)
            {
                cmd.Parameters.Add(op);
            }

            OracleDataAdapter oda = new OracleDataAdapter(cmd);
            oda.Fill(dataTable);
            return dataTable;
        }

        public static OracleDataReader Read(string sql, params OracleParameter[] parameters)
        {
            OpenDb();
            OracleCommand cmd = new OracleCommand(sql, _databaseController.con);
            foreach (OracleParameter op in parameters)
            {
                cmd.Parameters.Add(op);
            }

            OracleDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public static OracleDataReader Read(string sql, OracleTransaction transaction, params OracleParameter[] parameters)
        {
            OpenDb();
            OracleCommand cmd = new OracleCommand(sql, _databaseController.con);
            if (transaction != null)
            {
                cmd.Transaction = transaction;
            }
            foreach (OracleParameter op in parameters)
            {
                cmd.Parameters.Add(op);
            }

            OracleDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public static void Execute(string sql, params OracleParameter[] parameters)
        {
            OpenDb();
            OracleCommand cmd = new OracleCommand(sql, _databaseController.con);
            foreach (OracleParameter op in parameters)
            {
                cmd.Parameters.Add(op);
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
        }
        public static OracleParameter Executevratny(string sql, params OracleParameter[] parameters)
        {
            OpenDb();
            OracleCommand cmd = new OracleCommand(sql, _databaseController.con);
            foreach (OracleParameter op in parameters)
            {
                cmd.Parameters.Add(op);
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();

            // Assuming you know the name of the parameter you're interested in
            return cmd.Parameters["p_id_zamestnanec"] as OracleParameter;
        }

        public static void Execute1(string sql, params OracleParameter[] parameters)
        {
            OpenDb();
            OracleCommand cmd = new OracleCommand(sql, _databaseController.con);
            foreach (OracleParameter op in parameters)
            {
                cmd.Parameters.Add(op);
            }
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        public static void Execute(string sql, OracleTransaction transaction, params OracleParameter[] parameters)
        {
            OpenDb();
            OracleCommand cmd = new OracleCommand(sql, _databaseController.con);
            if (transaction != null)
            {
                cmd.Transaction = transaction;
            }
            foreach (OracleParameter op in parameters)
            {
                cmd.Parameters.Add(op);
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
        }

        public static OracleTransaction StartTransaction()
        {
            OpenDb();
            return _databaseController.con.BeginTransaction();
        }

        public static void Commit(OracleTransaction transaction)
        {
            OpenDb();
            transaction.Commit();
        }

        public static void Rollback(OracleTransaction transaction)
        {
            OpenDb();
            transaction.Rollback();
        }

        internal static void CloseDb()
        {
            if (_databaseController.con.State != ConnectionState.Closed)
            {
                _databaseController.con.Close();
            }
        }


        ~DatabaseController()
        {
            con.Dispose();
        }
    }
}