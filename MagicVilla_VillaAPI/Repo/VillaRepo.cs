using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repo.IRepo;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repo
{
    public class VillaRepo : Repo<Villa>,  IVillaRepo
    {
        private readonly ApplicationDbContext _db;
        public VillaRepo(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public async Task<Villa> Update(Villa entity)
        {
            entity.updatedDate = DateTime.Now;
            _db.Update(entity); await _db.SaveChangesAsync();
            return entity;
        }
    }
}
