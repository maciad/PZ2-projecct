using System.ComponentModel.DataAnnotations;

namespace Filmy.Models
{
    public class Uzytkownik
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Imie")]
        public string Imie { get; set; }

        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Hasło")]
        public string Haslo { get; set; }

        public ICollection<Ocena> ?Oceny { get; set; }
    }

}