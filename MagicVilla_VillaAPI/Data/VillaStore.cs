using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new()
        {
              new VillaDTO{ Id = 1, Name = "Gabriel", Sqft =100, Occupancy=2},
              new VillaDTO{ Id = 2, Name = "Ashely",  Sqft =200, Occupancy=2}
        };
    }
}
