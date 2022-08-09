using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repo.IRepo;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repo
{
    public class VillaRepo : IVillaRepo
    {
        private readonly ApplicationDbContext _db;
        public VillaRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(Villa entity)
        {
            await _db.AddAsync(entity); await Save();
        }

        public async Task<Villa> Get(Expression<Func<Villa, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Villa> query = _db.Villas;

            if (!tracked)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Villa>> GetAll(Expression<Func<Villa, bool>> filter = null)
        {
            IQueryable<Villa> query = _db.Villas;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task Remove(Villa entity)
        {
            _db.Remove(entity); await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task Update(Villa entity)
        {
            _db.Update(entity); await Save();
        }
    }
}
