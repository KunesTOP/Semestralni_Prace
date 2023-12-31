﻿using Back.databaze;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace Models.DatabaseControllers
{
    public class AsistentiController
    {
        public const string TABLE_NAME = "ASISTENTI";
        public const string ID_NAME = "id_zamestnanec";
        public const string PRAXE_NAME = "praxe";

        public static IEnumerable<int> GetIds()
        {
            return GetIds(TABLE_NAME, ID_NAME);
        }

        public static Asistent Get(int id)
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id));

            if (query.Rows.Count != 1)
            {
                return null;
            }

            return new Asistent()
            {
                Id = int.Parse(query.Rows[0][ID_NAME].ToString()),
                Praxe = int.Parse(query.Rows[0][PRAXE_NAME].ToString())
            };
        }

        //public static void InsertAsistent(Asistent asistent)
        //{
        //    DatabaseController.Execute1($"INSERT INTO {TABLE_NAME} ({ID_NAME}, {PRAXE_NAME}) VALUES (:id, :praxe)",
        //        new OracleParameter("id", asistent.IdZamestnanec),
        //        new OracleParameter("praxe", asistent.Praxe)
        //    );
        //}
        public static void DeleteAsistent(int id)
        {
            DatabaseController.Execute1($"DELETE FROM {TABLE_NAME} WHERE {ID_NAME} = :id",
                new OracleParameter("id", id)
            );
        }

        public static void UpsertAsistent(int id, JsonElement data)
        {
            OracleParameter idParam = new OracleParameter("p_id_zamestnanec", OracleDbType.Int32, ParameterDirection.InputOutput);
            idParam.Value = id;

            OracleParameter praxeParam = new OracleParameter("p_praxe", OracleDbType.Int32, ParameterDirection.InputOutput);

            // Kontrola a převod hodnoty praxe
            if (data.TryGetProperty("praxe", out JsonElement praxeElement))
            {
                if (praxeElement.ValueKind == JsonValueKind.Number && praxeElement.TryGetInt32(out int praxe))
                {
                    praxeParam.Value = praxe;
                }
                else if (praxeElement.ValueKind == JsonValueKind.String && int.TryParse(praxeElement.GetString(), out praxe))
                {
                    praxeParam.Value = praxe;
                }
                else
                {
                    throw new InvalidOperationException("Nepodařilo se přečíst hodnotu praxe jako celé číslo.");
                }
            }
            else
            {
                throw new InvalidOperationException("Element praxe nebyl nalezen v JSON data.");
            }

            ZamestnanciController.UpsertZamestnanec(id, data);
            DatabaseController.Execute("pkg_ostatni.upsert_asistent", idParam, praxeParam);
          
        }


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

        public static List<Asistent> GetAll()
        {
            DataTable query = DatabaseController.Query($"SELECT * FROM {TABLE_NAME}");
            List<Asistent> listAsistentu = new List<Asistent>();
            List<Zamestnanec> listZamestnancu = ZamestnanciController.GetAll();
            if (query.Rows.Count == 0)
            {
                return null;
            }
            try
            {
                foreach (DataRow dr in query.Rows)
                {
                    Zamestnanec zamestnanec = listZamestnancu.FirstOrDefault(z => z.Id == int.Parse(dr[ID_NAME].ToString()));

                    if (zamestnanec != null)
                    {
                        listAsistentu.Add(new Asistent()
                        {
                            Id = int.Parse(dr[ID_NAME].ToString()),
                            Jmeno = zamestnanec.Jmeno,
                            Prijmeni = zamestnanec.Prijmeni,
                            VeterKlinId = zamestnanec.VeterKlinId,
                            Profese = zamestnanec.Profese,
                            Praxe = int.Parse(dr[PRAXE_NAME].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listAsistentu;
        }
    }
}


