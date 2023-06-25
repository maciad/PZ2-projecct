using System.ComponentModel.DataAnnotations;

namespace Filmy.Models
{
    public class Ocena
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Film")]
        public Film? Film { get; set; }

        [Display(Name = "Użytkownik")]
        public Uzytkownik? Uzytkownik { get; set; }

        [Display(Name = "Ocena")]
        [Range(0, 10, ErrorMessage = "Ocena musi być w przedziale od 0 do 10.")]
        public int OcenaWartosc { get; set; }


    }
}