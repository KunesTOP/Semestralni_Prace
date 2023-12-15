using System.ComponentModel.DataAnnotations;

namespace Semestralni_prace.Models.Classes
{
    public class Registrovany
    {
        [Required(ErrorMessage = "Vyplňte prosím Jmeno")]
        public string Jmeno { get; set; }

        [Required(ErrorMessage = "Vyplňte prosím Prijmeni")]
        public string? Prijmeni { get; set; }

        [Required(ErrorMessage = "Vyplňte prosím Email")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Špatný email formát")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vyplňte prosím Ulici")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Vyplňte prosím Číslo popisné")]
        public int HouseNumber { get; set; }

        [Required(ErrorMessage = "Vyplňte prosím Město")]
        public string City { get; set; }

        [Required(ErrorMessage = "Vyplňtě prosím přihlašovací jméno")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must contain at least one letter and one digit, and be at least 8 characters long")]
        public  string Password { get; set; }

    }
}
