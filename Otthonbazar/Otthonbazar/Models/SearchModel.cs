using System.ComponentModel.DataAnnotations;

namespace Otthonbazar.Models
{
    public class SearchModel
    {
        [Display(Name = "Város azonosító")]
        public int? CityId { get; set; }
        [Display(Name = "Város")]
        public string CityName { get; set; }
        [Display(Name = "Maximum ár")]
        public decimal? PriceMax { get; set; }
        [Display(Name = "Minimum ár")]
        public decimal? PriceMin { get; set; }
        [Display(Name = "Maximum szobaszám")]
        public int? RoomMax { get; set; }
        [Display(Name = "Minimum szobaszám")]
        public int? RoomMin { get; set; }
        [Display(Name = "Maximum méret")]
        public int? SizeMax { get; set; }
        [Display(Name = "Minimum méret")]
        public int? SizeMin { get; set; }
    }
}
