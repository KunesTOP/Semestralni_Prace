
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models.DatabaseControllers
{
    public class RegisterController
    {
        private const string ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521)))(CONNECT_DATA=(SID=BDAS)));" +
            "user id=ST67057;password=abcde;" +
            "Connection Timeout=120;Validate connection=true;Min Pool Size=4;"; // Replace with your connection string

        public static void CreateRegisterEntry(string jmena, string prijmeni, string email, string mesto, string ulice, string cisloPopisne, string prihlasovaciJmeno, string prihlasovaciHeslo)
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                var command = new OracleCommand
                {
                    Connection = connection,
                    CommandText = @"INSERT INTO Register (jmena, prijmeni, email, mesto, ulice, cislo_popisne, prihlasovaci_jmeno, prihlasovaci_heslo) VALUES (:jmena, :prijmeni, :email, :mesto, :ulice, :cisloPopisne, :prihlasovaciJmeno, :prihlasovaciHeslo)",
                };
                prihlasovaciHeslo = PasswordHelper.HashPassword(prihlasovaciHeslo);
                command.Parameters.Add(new OracleParameter("jmena", jmena));
                command.Parameters.Add(new OracleParameter("prijmeni", prijmeni));
                command.Parameters.Add(new OracleParameter("email", email));
                command.Parameters.Add(new OracleParameter("mesto", mesto));
                command.Parameters.Add(new OracleParameter("ulice", ulice));
                command.Parameters.Add(new OracleParameter("cisloPopisne", cisloPopisne));
                command.Parameters.Add(new OracleParameter("prihlasovaciJmeno", prihlasovaciJmeno));
                command.Parameters.Add(new OracleParameter("prihlasovaciHeslo", prihlasovaciHeslo));

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static DataTable ReadRegisterEntry(string email)
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                var command = new OracleCommand
                {
                    Connection = connection,
                    CommandText = @"SELECT * FROM Register WHERE email = :email",
                };

                command.Parameters.Add(new OracleParameter("email", email));

                var adapter = new OracleDataAdapter(command);
                var dataTable = new DataTable();
                connection.Open();
                adapter.Fill(dataTable);

                return dataTable; // Return the DataTable containing the result
            }
        }

        public static void UpdateRegisterEntry(int id, string jmena, string prijmeni, string email, string mesto, string ulice, string cisloPopisne, string prihlasovaciJmeno, string prihlasovaciHeslo)
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                var command = new OracleCommand
                {
                    Connection = connection,
                    CommandText = @"UPDATE Register SET jmena = :jmena, prijmeni = :prijmeni, email = :email, mesto = :mesto, ulice = :ulice, cislo_popisne = :cisloPopisne, prihlasovaci_jmeno = :prihlasovaciJmeno, prihlasovaci_heslo = :prihlasovaciHeslo WHERE id = :id",
                };
                prihlasovaciHeslo = PasswordHelper.HashPassword(prihlasovaciHeslo);
                command.Parameters.Add(new OracleParameter("id", id));
                command.Parameters.Add(new OracleParameter("jmena", jmena));
                command.Parameters.Add(new OracleParameter("prijmeni", prijmeni));
                command.Parameters.Add(new OracleParameter("email", email));
                command.Parameters.Add(new OracleParameter("mesto", mesto));
                command.Parameters.Add(new OracleParameter("ulice", ulice));
                command.Parameters.Add(new OracleParameter("cisloPopisne", cisloPopisne));
                command.Parameters.Add(new OracleParameter("prihlasovaciJmeno", prihlasovaciJmeno));
                command.Parameters.Add(new OracleParameter("prihlasovaciHeslo", prihlasovaciHeslo));

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteRegisterEntry(int id)
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                var command = new OracleCommand
                {
                    Connection = connection,
                    CommandText = @"DELETE FROM Register WHERE id = :id",
                };

                command.Parameters.Add(new OracleParameter("id", id));

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static List<Dictionary<string, object>> GetAllRegisterEntries()
        {
            var list = new List<Dictionary<string, object>>();

            using (var connection = new OracleConnection(ConnectionString))
            {
                var command = new OracleCommand
                {
                    Connection = connection,
                    CommandText = @"SELECT * FROM Register",
                };

                var adapter = new OracleDataAdapter(command);
                var dataTable = new DataTable();
                connection.Open();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    var dict = new Dictionary<string, object>();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        dict[column.ColumnName] = row[column];
                    }
                    list.Add(dict);
                }
            }

            return list; // Return a list of dictionaries, each representing a row in the Register table
        }
    }
}
