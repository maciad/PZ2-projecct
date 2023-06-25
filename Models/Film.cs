using System.ComponentModel.DataAnnotations;

namespace Filmy.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tytuł")]
        public string Tytul { get; set; }

        [Display(Name = "Rok produkcji")]
        public int RokProdukcji { get; set; }

        [Display(Name = "Gatunek")]
        public string Gatunek { get; set; }

        [Display(Name = "Reżyser")]
        public string Rezyser { get; set; }

        [Display(Name = "Opis")]
        public string Opis { get; set; }

        [Display(Name = "Ocena")]
        public ICollection<Ocena> ?Oceny { get; set; }

        [Display(Name = "Główny aktor")]
        public Aktor? Aktor { get; set; }

        // [Display(Name = "Aktor drugoplanowy")]
        // public Aktor? Aktor2 { get; set; }

    }
}