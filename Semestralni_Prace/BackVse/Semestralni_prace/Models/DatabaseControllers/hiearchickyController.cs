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
            OracleParameter ageParam = new OracleParameter("p_birth_date", OracleDbType.Date, ParameterDirection.InputOutput);
            ageParam.Value = birthDate;

            DatabaseController.Execute("pkg_zbytek.calculate_age", ageParam);

            return Convert.ToInt32(ageParam.Value);
        }

        public static string GetVetTitle(int vetId)
        {
            OracleParameter titleParam = new OracleParameter("p_vet_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            titleParam.Value = vetId;

            DatabaseController.Execute("pkg_zbytek.get_vet_title", titleParam);

            return titleParam.Value.ToString();
        }

        public static string CheckVaccineStatus(int animalId)
        {
            OracleParameter statusParam = new OracleParameter("p_animal_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            statusParam.Value = animalId;

            DatabaseController.Execute("pkg_zbytek.check_vaccine_status", statusParam);

            return statusParam.Value.ToString();
        }

        public static void AddNewAnimal(string name, string gender, DateTime birthDate, int ownerId, int rasaId)
        {
            OracleParameter nameParam = new OracleParameter("p_name", OracleDbType.Varchar2, ParameterDirection.InputOutput);
            nameParam.Value = name;

            OracleParameter genderParam = new OracleParameter("p_gender", OracleDbType.Varchar2, ParameterDirection.InputOutput);
            genderParam.Value = gender;

            OracleParameter birthDateParam = new OracleParameter("p_birth_date", OracleDbType.Date, ParameterDirection.InputOutput);
            birthDateParam.Value = birthDate;

            OracleParameter ownerIdParam = new OracleParameter("p_owner_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            ownerIdParam.Value = ownerId;

            OracleParameter rasaIdParam = new OracleParameter("p_rasa_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            rasaIdParam.Value = rasaId;

            DatabaseController.Execute("pkg_zbytek.add_new_animal", nameParam, genderParam, birthDateParam, ownerIdParam, rasaIdParam);
        }

        public static void UpdateVetProfese(int vetId, string newProfese)
        {
            OracleParameter vetIdParam = new OracleParameter("p_vet_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            vetIdParam.Value = vetId;

            OracleParameter profeseParam = new OracleParameter("p_new_profese", OracleDbType.Varchar2, ParameterDirection.InputOutput);
            profeseParam.Value = newProfese;

            DatabaseController.Execute("pkg_zbytek.update_vet_profese", vetIdParam, profeseParam);
        }

        public static void CreateNewOwnerWithCard(string name, string surname, string email, string phone, int addressId, int cardNumber, int chipNumber)
        {
            OracleParameter nameParam = new OracleParameter("p_name", OracleDbType.Varchar2, ParameterDirection.InputOutput);
            nameParam.Value = name;

            OracleParameter surnameParam = new OracleParameter("p_surname", OracleDbType.Varchar2, ParameterDirection.InputOutput);
            surnameParam.Value = surname;

            OracleParameter emailParam = new OracleParameter("p_email", OracleDbType.Varchar2, ParameterDirection.InputOutput);
            emailParam.Value = email;

            OracleParameter phoneParam = new OracleParameter("p_phone", OracleDbType.Varchar2, ParameterDirection.InputOutput);
            phoneParam.Value = phone;

            OracleParameter addressIdParam = new OracleParameter("p_address_id", OracleDbType.Int32, ParameterDirection.InputOutput);
            addressIdParam.Value = addressId;

            OracleParameter cardNumberParam = new OracleParameter("p_card_number", OracleDbType.Int32, ParameterDirection.InputOutput);
            cardNumberParam.Value = cardNumber;

            OracleParameter chipNumberParam = new OracleParameter("p_chip_number", OracleDbType.Int32, ParameterDirection.InputOutput);
            chipNumberParam.Value = chipNumber;

            DatabaseController.Execute("pkg_zbytek.create_new_owner_with_card", nameParam, surnameParam, emailParam, phoneParam, addressIdParam, cardNumberParam, chipNumberParam);
        }

      
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
        public static void HierarchicalQueryProcedure()
        {
            DatabaseController.Execute("pkg_zbytek.HierarchicalQueryProcedure");
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
    }
}
