using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto
{
    public class VillaCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int? Occupancy { get; set; }
        public int? Sqft { get; set; }
        public string? Details { get; set; }
        [Required]
        public int Rate { get; set; }
        public string? ImageURL { get; set; }
        public string? Amenity { get; set; }
    }
}
