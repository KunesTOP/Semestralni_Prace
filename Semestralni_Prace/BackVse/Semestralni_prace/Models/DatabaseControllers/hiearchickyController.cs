using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Semestralni_prace.Models.DatabaseControllers
{
    public class HiearchickyController
    {
        public static void GetAnimalsForOwnerExplicit(int pOwnerId)
        {
            DatabaseController.Execute("pkg_zbytek.get_animals_for_owner_explicit", new OracleParameter("p_owner_id", pOwnerId));
        }

        public static void GetAnimalsForOwner(int pOwnerId)
        {
            DatabaseController.Execute("pkg_zbytek.get_animals_for_owner", new OracleParameter("p_owner_id", pOwnerId));
        }

        public static int CalculateAge(DateTime birthDate)
        {
            string sql = "SELECT FLOOR(MONTHS_BETWEEN(SYSDATE, :birthDate) / 12) AS age FROM DUAL";

            using (OracleConnection conn = new OracleConnection(DatabaseController.CONSTR))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    // Přidání parametru pro datum narození
                    cmd.Parameters.Add(new OracleParameter("birthDate", OracleDbType.Date) { Value = birthDate });

                    // Spuštění dotazu a čtení výsledku
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Přečtení výsledku a jeho konverze na integer
                            return Convert.ToInt32(reader["age"]);
                        }
                        else
                        {
                            throw new Exception("Nepodařilo se získat věk.");
                        }
                    }
                }
            }
        }

        public static string GetVetTitle(int vetId)

        {
            string sql = @"SELECT t.nazev_titul
                   FROM zamestnanci_maji_tituly mt
                   JOIN tituly t ON mt.titul_id_titul = t.id_titul
                   WHERE mt.zames_id_zames = :vetId
                   AND ROWNUM = 1";

            // Nastavení parametru vetId
            OracleParameter vetIdParam = new OracleParameter("vetId", OracleDbType.Int32, vetId, ParameterDirection.Input);

            // Volání metody Query
            DataTable dt = DatabaseController.Query(sql, vetIdParam);

            // Kontrola, zda byl nějaký řádek vrácen
            if (dt.Rows.Count > 0)
            {
                // Vrácení názvu titulu
                return dt.Rows[0]["nazev_titul"].ToString();
            }
            else
            {
                // V případě, že nebyl nalezen žádný titul
                return "Titul nenalezen";
            }
        }


        public static string CheckVaccineStatus(int animalId)


        {
            string sql = @"SELECT CASE WHEN COUNT(*) > 0 THEN 'Complete' ELSE 'Incomplete' END 
                   FROM vakcina_podavana_zvireti 
                   WHERE zvire_id_zvire = :animalId";

            OracleParameter animalIdParam = new OracleParameter("animalId", OracleDbType.Int32, animalId, ParameterDirection.Input);

            // Vykonání dotazu a získání výsledku
            DataTable dataTable = DatabaseController.Query(sql, animalIdParam);
            if (dataTable.Rows.Count > 0)
            {
                return dataTable.Rows[0][0].ToString(); // Vrací 'Complete' nebo 'Incomplete'
            }

            return "Data not found"; // V případě, že nebyly nalezeny žádné záznamy
        }


        //nepouzivej
        public static void UpdateVetProfese(int vetId, string newProfese)
        {
            OracleParameter vetIdParam = new OracleParameter("p_vet_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            vetIdParam.Value = vetId;

            OracleParameter profeseParam = new OracleParameter("p_new_profese", OracleDbType.Varchar2, ParameterDirection.InputOutput);
            profeseParam.Value = newProfese;

            DatabaseController.Execute("pkg_zbytek.update_vet_profese", vetIdParam, profeseParam);
        }
      //nepouzivej
        public static void UpdateOwnerAddress(int ownerId, int newAddressId)
        {
            OracleParameter ownerIdParam = new OracleParameter("p_owner_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            ownerIdParam.Value = ownerId;

            OracleParameter newAddressIdParam = new OracleParameter("p_new_address_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            newAddressIdParam.Value = newAddressId;

            DatabaseController.Execute("pkg_zbytek.update_owner_address", ownerIdParam, newAddressIdParam);
        }
        public static void GetTableColumns()
        {
            DatabaseController.Execute("pck_zbytekinfo.get_table_columns");
        }
        static void GetTables()
        {
            DatabaseController.Execute("pck_zbytekinfo.get_tables");
        }
        public static void GetViews()
        {
            DatabaseController.Execute("pck_zbytekinfo.get_views");
        }

        public static void GetProcedures()
        {
            DatabaseController.Execute("pck_zbytekinfo.get_procedures");
        }

        public static void GetFunctions()
        {
            DatabaseController.Execute("pck_zbytekinfo.get_functions");
        }

        public static void GetTriggers()
        {
            DatabaseController.Execute("pck_zbytekinfo.get_triggers");
        }

        public static void GetIndexes()
        {
            DatabaseController.Execute("pck_zbytekinfo.get_indexes");
        }

        public static void GetSequences()
        {
            DatabaseController.Execute("pck_zbytekinfo.get_sequences");
        }

        public static void GetConstraints()
        {
            DatabaseController.Execute("pck_zbytekinfo.get_constraints");
        }

        public static void GetSynonyms()
        {
            DatabaseController.Execute("pck_zbytekinfo.get_synonyms");
        }

        public static void GetCreated()
        {
            DatabaseController.Execute("pck_zbytekinfo.get_created");
        }
        public static void HierarchicalQueryProcedure(int startId)
        {
            // Nastavení vstupního parametru
            OracleParameter p_start_id = new OracleParameter("p_start_id", OracleDbType.Int32, startId, ParameterDirection.Input);

            // Volání procedury
            DatabaseController.Execute("pkg_zbytek.HierarchicalQueryProcedure", p_start_id);

            // Další logika, pokud je potřeba...
        }
        public static void GetNames()
        {
            
            DatabaseController.Execute("pck_zbytekinfo.get_names('zamestnanci')");
        }

        public static void SchvaleniUctu(string jmeno, string prijmeni, string email, string mesto, string ulice, string cisloPopisne, string prihlasovaciJmeno, string prihlasovaciHeslo, int urovenAutorizace)
        {
            // Parametry pro proceduru
            OracleParameter p_jmeno = new OracleParameter("p_jmeno", OracleDbType.Varchar2, jmeno, ParameterDirection.Input);
            OracleParameter p_prijmeni = new OracleParameter("p_prijmeni", OracleDbType.Varchar2, prijmeni, ParameterDirection.Input);
            OracleParameter p_email = new OracleParameter("p_email", OracleDbType.Varchar2, email, ParameterDirection.Input);
            OracleParameter p_mesto = new OracleParameter("p_mesto", OracleDbType.Varchar2, mesto, ParameterDirection.Input);
            OracleParameter p_ulice = new OracleParameter("p_ulice", OracleDbType.Varchar2, ulice, ParameterDirection.Input);
            OracleParameter p_cisloPopisne = new OracleParameter("p_cislo_popisne", OracleDbType.Varchar2, cisloPopisne, ParameterDirection.Input);
            OracleParameter p_prihlasovaciJmeno = new OracleParameter("p_prihlasovaci_jmeno", OracleDbType.Varchar2, prihlasovaciJmeno, ParameterDirection.Input);
            OracleParameter p_prihlasovaciHeslo = new OracleParameter("p_prihlasovaci_heslo", OracleDbType.Varchar2, prihlasovaciHeslo, ParameterDirection.Input);
            OracleParameter p_urovenAutorizace = new OracleParameter("p_uroven_autorizace", OracleDbType.Int32, urovenAutorizace, ParameterDirection.Input);

            // Volání procedury
            DatabaseController.Execute("pkg_zbytek.schvaleni_uctu", p_jmeno, p_prijmeni, p_email, p_mesto, p_ulice, p_cisloPopisne, p_prihlasovaciJmeno, p_prihlasovaciHeslo, p_urovenAutorizace);

        }
        public static void PridatDokumentZamestnanci(int idZamestnanec, int idDokument)
        {
            // Parametry pro proceduru
            OracleParameter p_id_zamestnanec = new OracleParameter("p_id_zamestnanec", OracleDbType.Int32, idZamestnanec, ParameterDirection.Input);
            OracleParameter p_id_dokument = new OracleParameter("p_id_dokument", OracleDbType.Int32, idDokument, ParameterDirection.Input);

            // Volání procedury
            DatabaseController.Execute("pkg_zbytek.pridat_dokument_zamestnanci", p_id_zamestnanec, p_id_dokument);
        }

        public static void NajdiPodrobnostiZamestnance(string jmenoUctu, out string jmeno, out string prijmeni, out string profese, out string nazevDokumentu, out byte[] dokumentBlobs, out string jmenoMajitel, out string prijmeniMajitel, out int adresaKlinika, out string mesto, out string ulice, out string cisloPopisne)
        {
            // Nastavení vstupního parametru
            OracleParameter p_jmeno_uctu = new OracleParameter("p_jmeno_uctu", OracleDbType.Varchar2, jmenoUctu, ParameterDirection.Input);

            // Nastavení výstupních parametrů
            OracleParameter o_jmeno = new OracleParameter("o_jmeno", OracleDbType.Varchar2, ParameterDirection.Output);
            o_jmeno.Size = 32; // Velikost odpovídající sloupci v databázi
            OracleParameter o_prijmeni = new OracleParameter("o_prijmeni", OracleDbType.Varchar2, ParameterDirection.Output);
            o_prijmeni.Size = 64;
            OracleParameter o_profese = new OracleParameter("o_profese", OracleDbType.Varchar2, ParameterDirection.Output);
            o_profese.Size = 32;
            OracleParameter o_nazev_dokumentu = new OracleParameter("o_nazev_dokumentu", OracleDbType.Varchar2, ParameterDirection.Output);
            o_nazev_dokumentu.Size = 30;
            OracleParameter o_dokument_blobs = new OracleParameter("o_dokument_blobs", OracleDbType.Blob, ParameterDirection.Output);
            OracleParameter o_jmeno_majitel = new OracleParameter("o_jmeno_majitel", OracleDbType.Varchar2, ParameterDirection.Output);
            o_jmeno_majitel.Size = 32;
            OracleParameter o_prijmeni_majitel = new OracleParameter("o_prijmeni_majitel", OracleDbType.Varchar2, ParameterDirection.Output);
            o_prijmeni_majitel.Size = 64;
            OracleParameter o_adresa_klinika = new OracleParameter("o_adresa_klinika", OracleDbType.Int32, ParameterDirection.Output);
            // Přidání OUT parametrů pro adresu
            OracleParameter o_mesto = new OracleParameter("o_mesto", OracleDbType.Varchar2, ParameterDirection.Output);
            o_mesto.Size = 64;
            OracleParameter o_ulice = new OracleParameter("o_ulice", OracleDbType.Varchar2, ParameterDirection.Output);
            o_ulice.Size = 128;
            OracleParameter o_cislo_popisne = new OracleParameter("o_cislo_popisne", OracleDbType.Int32, ParameterDirection.Output);

            // Zbytek kódu...

            // Volání procedury
            DatabaseController.Execute("pkg_zbytek.najdi_podrobnosti_zamestnance",
        p_jmeno_uctu,
        o_jmeno,
        o_prijmeni,
        o_profese,
        o_nazev_dokumentu,
        o_dokument_blobs,
        o_jmeno_majitel,
        o_prijmeni_majitel,
        o_adresa_klinika,
        o_mesto,
        o_ulice,
        o_cislo_popisne);

            // Získání výsledků z výstupních parametrů
            jmeno = o_jmeno.Value as string;
            prijmeni = o_prijmeni.Value as string;
            profese = o_profese.Value as string;
            nazevDokumentu = o_nazev_dokumentu.Value as string;
            dokumentBlobs = (byte[])(o_dokument_blobs.Value ?? new byte[0]); // Přetypování na byte[] pro práci s BLOB daty
            jmenoMajitel = o_jmeno_majitel.Value as string;
            prijmeniMajitel = o_prijmeni_majitel.Value as string;
            adresaKlinika = Convert.ToInt32(o_adresa_klinika.Value);
            mesto = o_mesto.Value as string;
            ulice = o_ulice.Value as string;
            cisloPopisne = Convert.ToString(o_cislo_popisne.Value);
            if (o_jmeno.Status == OracleParameterStatus.NullFetched)
            {
                jmeno = string.Empty;
            }
        }
        public static void NajdiZamestnancePodleKliniky(int veterKlinId)
        {
            OracleParameter p_veter_klin_id = new OracleParameter("p_veter_klin_id", OracleDbType.Int32, veterKlinId, ParameterDirection.Input);
            OracleParameter o_zamestnanci = new OracleParameter("o_zamestnanci", OracleDbType.RefCursor, ParameterDirection.Output);

            DatabaseController.Execute("najdi_zamestnance_podle_kliniky", p_veter_klin_id, o_zamestnanci);
        }

    }
}
