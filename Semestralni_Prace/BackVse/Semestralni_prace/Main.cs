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

        
        Console.WriteLine($"List {LekyController.TABLE_NAME}:");

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
        VakcinyController.InsertVakcina(newVakcina);
        Console.WriteLine("Tabulka pred smazanim");
        query = $"SELECT * FROM {VakcinyController.TABLE_NAME}";
        result = DatabaseController.Query(query);

        Console.WriteLine($"List {VakcinyController.TABLE_NAME}:");

        foreach (DataRow row in result.Rows)
        {
            Console.WriteLine($"{VakcinyController.ID_VAKCINA_NAME}: {row[VakcinyController.ID_VAKCINA_NAME]}, {VakcinyController.NAZEV_VAKCINA_NAME}: {row[VakcinyController.NAZEV_VAKCINA_NAME]}");
        }
        int idToRemove = 1532453;
       VakcinyController.RemoveVakcina(idToRemove);
        Console.WriteLine("Tabulka po smazani");
       
        query = $"SELECT * FROM {VakcinyController.TABLE_NAME}";
         result = DatabaseController.Query(query);

        Console.WriteLine($"List {VakcinyController.TABLE_NAME}:");

        foreach (DataRow row in result.Rows)
        {
            Console.WriteLine($"{VakcinyController.ID_VAKCINA_NAME}: {row[VakcinyController.ID_VAKCINA_NAME]}, {VakcinyController.NAZEV_VAKCINA_NAME}: {row[VakcinyController.NAZEV_VAKCINA_NAME]}");
        }
        //titul

        Titul newTitul = new Titul
        {
            IdTitul = 10,
            ZkratkaTitul = "Mgr.",
            NazevTitul = "Magistr"
        };
        idToRemove = 10;
        TitulyController.RemoveTitul(idToRemove);
        idToRemove = 20;
        TitulyController.RemoveTitul(idToRemove);

        TitulyController.InsertTitul(newTitul);


        query = $"SELECT * FROM {TitulyController.TABLE_NAME}";
         result = DatabaseController.Query(query);

        Console.WriteLine($"List {TitulyController.TABLE_NAME}:");

        foreach (DataRow row in result.Rows)
        {
            Console.WriteLine($"{TitulyController.ID_TITUL_NAME}: {row[TitulyController.ID_TITUL_NAME]}, {TitulyController.ZKRATKA_TITUL_NAME}: {row[TitulyController.ZKRATKA_TITUL_NAME]}, {TitulyController.NAZEV_TITUL_NAME}: {row[TitulyController.NAZEV_TITUL_NAME]}");
        }
        Titul newTitul1 = new Titul
        {
            IdTitul = 10,
            NazevTitul = "psycholog",
            ZkratkaTitul = "PHD"
        };
        TitulyController.InsertTitul(newTitul1);
        Console.WriteLine("Tituly po uprave");
        query = $"SELECT * FROM {TitulyController.TABLE_NAME}";
        result = DatabaseController.Query(query);

        Console.WriteLine($"List {TitulyController.TABLE_NAME}:");

        foreach (DataRow row in result.Rows)
        {
            Console.WriteLine($"{TitulyController.ID_TITUL_NAME}: {row[TitulyController.ID_TITUL_NAME]}, {TitulyController.ZKRATKA_TITUL_NAME}: {row[TitulyController.ZKRATKA_TITUL_NAME]}, {TitulyController.NAZEV_TITUL_NAME}: {row[TitulyController.NAZEV_TITUL_NAME]}");
        }

        idToRemove = 10;
        TitulyController.RemoveTitul(idToRemove);

       
        result = DatabaseController.Query(query);

        Console.WriteLine($"List {TitulyController.TABLE_NAME} after removal:");

        foreach (DataRow row in result.Rows)
        {
            Console.WriteLine($"{TitulyController.ID_TITUL_NAME}: {row[TitulyController.ID_TITUL_NAME]}, {TitulyController.ZKRATKA_TITUL_NAME}: {row[TitulyController.ZKRATKA_TITUL_NAME]}, {TitulyController.NAZEV_TITUL_NAME}: {row[TitulyController.NAZEV_TITUL_NAME]}");
        }

        
        Console.ReadLine();
    }
}
