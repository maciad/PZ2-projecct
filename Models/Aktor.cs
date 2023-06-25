using System.ComponentModel.DataAnnotations;

namespace Filmy.Models
{
    public class Aktor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Imie")]
        public string Imie { get; set; }

        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data urodzenia")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataUrodzenia { get; set; }

        [Display(Name = "Kraj pochodzenia")]
        public string KrajPochodzenia { get; set; }
        
        public ICollection<Film> ?Films { get; set; }
    }

}