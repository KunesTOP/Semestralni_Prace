using System.ComponentModel.DataAnnotations;

namespace Semestralni_prace.Models.Classes
{
    public class Registrovany
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Vyplňte prosím Jmeno")]
        public string Jmeno { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Vyplňte prosím Prijmeni")]
        public string? Prijmeni { get; set; }

        [Required(ErrorMessage = "Vyplňte prosím Email"), DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Špatný email formát")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Vyplňte prosím Ulici")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Vyplňte prosím Číslo popisné")]
        public int HouseNumber { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Vyplňte prosím Město")]
        public string City { get; set; }


        [Required(ErrorMessage = "Vyplňtě prosím přihlašovací jméno")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Heslo je vyžadováno")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Heslo musí obsahovat jedno číslo a jedno písmeno a musí být aspoň 8 znaků dlouhá.")]
        public  string Password { get; set; }

    }
}
