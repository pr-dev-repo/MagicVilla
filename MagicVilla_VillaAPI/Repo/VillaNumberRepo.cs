using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repo.IRepo;

namespace MagicVilla_VillaAPI.Repo
{
    public class VillaNumberRepo : Repo<VillaNumber>,  IVillaNumberRepo
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberRepo(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public async Task<VillaNumber> Update(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.VillaNumbers.Update(entity); await _db.SaveChangesAsync();
            return entity;
        }
    }
}
