using System;

public class Class1
{
    public static void Main()
    {
        // Example query to retrieve all addresses
        string query = $"SELECT * FROM {AnamnezaUrCujeNemocController.TABLE_NAME}";

        // Execute the query and get the result as a DataTable
        var result = DatabaseController.Query(query);

        // Print the addresses
        Console.WriteLine($"List of {AnamnezaUrCujeNemocController.TABLE_NAME}:");

        foreach (DataRow row in result.Rows)
        {
            Console.WriteLine(row[AnamnezaUrCujeNemocController.NEMOC_ID_NAME]); // Replace "AddressColumnName" with the actual column name in your "adress" table
        }

        // Insert a new lek
        Lek newLek = new Lek { Id = 1, Nazev = "NovyLek" };
        AnamnezaUrCujeNemocController.InsertLek(newLek);

        // Close the database connection (optional, as it's done in your DatabaseController class)
        DatabaseController.CloseDb();

        // Pause to see the output
        Console.ReadLine();
    }
}
