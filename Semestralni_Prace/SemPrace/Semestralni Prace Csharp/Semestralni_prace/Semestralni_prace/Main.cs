using Back.Controllers;
using Back.databaze;
using Semestralni_Práce.Classes;
using System;
using System.Data;

public class Class1
{
    public static void Main()
    {
       
        string query = $"SELECT * FROM {LekyController.TABLE_NAME}";

        
        var result = DatabaseController.Query(query);

        
        Console.WriteLine($"List of {LekyController.TABLE_NAME}:");

        foreach (DataRow row in result.Rows)
        {
            Console.WriteLine(row[LekyController.NAZEV_NAME]); 
        }


        Majitel newMajitel = new Majitel
        {
            PacientId = 100,
            Mail = "example@email.com",
            Telefon = "123456789",
            Jmeno = "John",
            Prijmeni = "Doe",
            VetKlinId = 2,
            IdMajitel = 3
        };

        

        //MajiteleZviratController.InsertMajitel(newMajitel);


        Vakcina newVakcina = new Vakcina
        {
            IdVakcina = 1532453,
            NazevVakcina = "covid lul"
        };
       // int idToRemove = 1532453;
       // VakcinyController.RemoveVakcina(idToRemove);
        //VakcinyController.InsertVakcina(newVakcina);
        query = $"SELECT * FROM {VakcinyController.TABLE_NAME}";
         result = DatabaseController.Query(query);

        Console.WriteLine($"List of {VakcinyController.TABLE_NAME}:");

        foreach (DataRow row in result.Rows)
        {
            Console.WriteLine($"{VakcinyController.ID_VAKCINA_NAME}: {row[VakcinyController.ID_VAKCINA_NAME]}, {VakcinyController.NAZEV_VAKCINA_NAME}: {row[VakcinyController.NAZEV_VAKCINA_NAME]}");
        }

        // Pozastavení prohlížeče, aby ses mohl podívat na výstup
        Console.ReadLine();
    }
}
