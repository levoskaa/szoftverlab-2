using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otthonbazar.Data
{
    public class City
    {
        public int Id { get; set; }
        [Display(Name = "Város")]
        [Required(ErrorMessage = "Az irányítószám mező kitöltése kötelező."), StringLength(4, MinimumLength = 4, ErrorMessage = "Az irányítószám 4 db számból kell álljon.")]
        public string Zip { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; }
    }
}
