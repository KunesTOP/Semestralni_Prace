using Back.databaze;
using Oracle.ManagedDataAccess.Client;
using Semestralni_prace.Models.Classes;
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
        public static DataTable GetTableColumns()
        {
            // SQL dotaz pro získání informací o sloupcích všech tabulek
            string sql = @"
        SELECT 
            TABLE_NAME, 
            COLUMN_NAME, 
            DATA_TYPE, 
            DATA_LENGTH 
        FROM USER_TAB_COLUMNS"; // Nebo ALL_TAB_COLUMNS, DBA_TAB_COLUMNS podle oprávnění

            return DatabaseController.Query(sql);
        }

        public static DataTable GetTables()
        {
            string sql = "SELECT table_name FROM all_tables"; // nebo "SELECT table_name FROM all_tables" pro všechny tabulky
            return DatabaseController.Query(sql);

        }

        public static async Task<DataTable> GetViews()
        {
            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521)))(CONNECT_DATA=(SID=BDAS)));" +
            "user id=ST67057;password=abcde;" +
            "Connection Timeout=120;Validate connection=true;Min Pool Size=4;";

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();
                DataTable dataTable = new DataTable();

                using (OracleCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT view_name FROM all_views"; // Získání názvů všech pohledů
                    OracleDataAdapter dataAdapter = new OracleDataAdapter(command);
                    dataAdapter.Fill(dataTable);
                }

                return dataTable;
            }
        }



        public static DataTable GetProcedures()
        {
            string sql = "SELECT OBJECT_NAME FROM USER_PROCEDURES";
            return DatabaseController.Query(sql);
        }


        public static DataTable GetFunctions()
        {
            string sql = "SELECT OBJECT_NAME FROM USER_FUNCTIONS"; // Nebo ALL_FUNCTIONS, DBA_FUNCTIONS podle oprávnění
            return DatabaseController.Query(sql);
        }


        public static DataTable GetTriggers()
        {
            string sql = "SELECT TRIGGER_NAME FROM USER_TRIGGERS"; // Nebo ALL_TRIGGERS, DBA_TRIGGERS podle oprávnění
            return DatabaseController.Query(sql);
        }


        public static DataTable GetIndexes()
        {
            string sql = "SELECT INDEX_NAME FROM USER_INDEXES"; // Nebo ALL_INDEXES, DBA_INDEXES podle oprávnění
            return DatabaseController.Query(sql);
        }


        public static DataTable GetSequences()
        {
            string sql = "SELECT SEQUENCE_NAME FROM USER_SEQUENCES"; // Nebo ALL_SEQUENCES, DBA_SEQUENCES podle oprávnění
            return DatabaseController.Query(sql);
        }


        public static DataTable GetConstraints()
        {
            string sql = "SELECT CONSTRAINT_NAME FROM USER_CONSTRAINTS"; // Nebo ALL_CONSTRAINTS, DBA_CONSTRAINTS podle oprávnění
            return DatabaseController.Query(sql);
        }


        public static DataTable GetSynonyms()
        {
            string sql = "SELECT SYNONYM_NAME FROM USER_SYNONYMS"; // Nebo ALL_SYNONYMS, DBA_SYNONYMS podle oprávnění
            return DatabaseController.Query(sql);
        }

        public static DataTable GetCreated()
        {
            string sql = "SELECT OBJECT_NAME, CREATED FROM USER_OBJECTS WHERE OBJECT_TYPE = 'TABLE'"; // Můžete filtrovat podle typu objektu
            return DatabaseController.Query(sql);
        }

        public static DataTable HierarchicalQueryProcedure(int startId)
        {
            // Nastavení vstupního parametru
            OracleParameter p_start_id = new OracleParameter("p_start_id", OracleDbType.Int32, startId, ParameterDirection.Input);

           return DatabaseController.Query("pkg_zbytek.HierarchicalQueryProcedure", p_start_id);
        }
        public List<Zamestnanec> GetAllPodrizeni(DataTable table)
        {
            if (table.Rows.Count == 0)
            {
                return null;
            }
            List<Zamestnanec> listZamestnancu = new List<Zamestnanec>();
            foreach (DataRow dr in table.Rows)
            {
                listZamestnancu.Add(new Zamestnanec
                {
                    Id = int.Parse(dr[0].ToString()),
                    Jmeno = dr[0].ToString(),
                    Prijmeni = dr[0].ToString(),
                    Profese = dr[0].ToString(),
                    VeterKlinId = int.Parse(dr[0].ToString())
                });
            }

            return listZamestnancu;
        }

        public static DataTable GetNames()
        {
            string sql = "SELECT jmeno, prijmeni FROM zamestnanci"; // Předpokládám, že jmeno a prijmeni jsou sloupce v tabulce zamestnanci
            return DatabaseController.Query(sql);
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

        public static List<Zamestnanec> NajdiZamestnancePodleKliniky(int? veterKlinId)
        {
            DataTable table = DatabaseController.Query($" SELECT id_zamestnanec, jmeno, prijmeni, veter_klin_id, profese FROM zamestnanci WHERE veter_klin_id = :p_veter_klin_id",
            new OracleParameter("p_veter_klin_id", veterKlinId));

            List<Zamestnanec> listZamestnancu = new List<Zamestnanec>();
            foreach (DataRow row in table.Rows)
            {
                listZamestnancu.Add(new Zamestnanec
                {
                    Id = int.Parse(row["id_zamestnanec"].ToString()),
                    Jmeno = row["jmeno"].ToString(),
                    Prijmeni = row["prijmeni"].ToString(),
                    VeterKlinId = int.Parse(row["veter_klin_id"].ToString()),
                    Profese = row["profese"].ToString()
                });
            }
            return listZamestnancu;
        }

    }
}
