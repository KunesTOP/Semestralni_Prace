using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using Semestralni_Práce.Classes;
using System.Data;

public static class AnamnezyController //TODO knihovny budu psat dolu jakoby 
{
    public const string TABLE_NAME = "ANAMNEZY";
    public const string ID_NAME = "id_anamneza";
    public const string DATUM_NAME = "datum_anamneza";

    public static IEnumerable<int> GetIds()
    {
        return GetIds(TABLE_NAME, ID_NAME);
    }

    public static Anamneza Get(int id)
    {
        DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
            new OracleParameter("id", id));

        if (query.Rows.Count != 1)
        {
            return null;
        }

        return new Anamneza()
        {
            Id = int.Parse(query.Rows[0][ID_NAME].ToString()),
            Datum = DateTime.Parse(query.Rows[0][DATUM_NAME].ToString())//todo ??
        };
    }

    public static void InsertAnamneza(Anamneza anamneza)
    {
        DatabaseController.Execute($"INSERT INTO {TABLE_NAME} ({ID_NAME}, {DATUM_NAME}) VALUES (:id, :datum)",
            new OracleParameter("id", anamneza.Id),
            new OracleParameter("datum", anamneza.Datum)
        );
    }

    // Další metody podle potřeby...

    // Tato metoda získá seznam ID podle zadaných podmínek
    private static IEnumerable<int> GetIds(string tableName, string idColumnName)
    {
        List<int> ids = new List<int>();

        DataTable query = DatabaseController.Query($"SELECT {idColumnName} FROM {tableName}");

        foreach (DataRow dr in query.Rows)
        {
            ids.Add(int.Parse(dr[idColumnName].ToString()));
        }

        return ids;
    }
}


