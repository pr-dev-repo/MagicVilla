using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaAPI.Models
{
    public class Villa
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Details { get; set; }
            public int Rate { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime updatedDate { get; set; }
            public int Occupancy { get; set; }
            public int Sqft { get; set; }
            public string ImageURL { get; set; }
            public string Amenity { get; set; }
    }
}
