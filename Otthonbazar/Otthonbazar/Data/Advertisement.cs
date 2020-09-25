using System.ComponentModel.DataAnnotations;

namespace Otthonbazar.Data
{
    public class Advertisement
    {
        public int Id { get; set; }
        [Display(Name = "Cím")]
        [Required(ErrorMessage = "A cím mező kitöltése kötelező.")]
        public string Address { get; set; }
        [Display(Name = "Építés éve")]
        [Required(ErrorMessage = "Az építés éve mező kitöltése kötelező.")]
        public int BuildDate { get; set; }
        public City City { get; set; }
        [Display(Name = "Város")]
        [Required(ErrorMessage = "A város mező kitöltése kötelező.")]
        public int CityId { get; set; }
        [Display(Name = "Leírás")]
        public string Description { get; set; }
        [Display(Name = "Fél szobák")]
        [Required(ErrorMessage = "A fél szobák mező kitöltése kötelező.")]
        public int HalfRoom { get; set; }
        [Display(Name = "Fénykép")]
        public string ImageUrl { get; set; }
        [Display(Name = "Eladási ár")]
        [Required(ErrorMessage = "Az ár mező kitöltése kötelező.")]
        public decimal Price { get; set; }
        [Display(Name = "Szobák")]
        [Required(ErrorMessage = "A szobák mező kitöltése kötelező.")]
        public int Room { get; set; }
        [Display(Name = "Alapterület")]
        [Required(ErrorMessage = "Az alapterület mező kitöltése kötelező.")]
        public int Size { get; set; }
        [Display(Name = "Típus")]
        [Required(ErrorMessage = "A típus mező kitöltése kötelező.")]
        public AdvertisementType AdvertisementType { get; set; }
    }
}
