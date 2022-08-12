using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repo.IRepo;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repo
{
    public class Repo<T> : IRepo<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;

        public Repo(ApplicationDbContext db)
        {
            _db = db;
            dbset = db.Set<T>();
        }
        public async Task Create(T entity)
        {
            await dbset.AddAsync(entity); await Save();
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProps = null)
        {
            IQueryable<T> query = dbset;

            if (!tracked)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (includeProps != null)
            {
                foreach (var item in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProps = null, int pageSize = 0, int pageNumber = 1)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
                query = query.Where(filter);

            if (pageSize > 0) // PAGINATION
            {
                if (pageSize > 100)
                {
                    pageSize = 100;
                }

                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }

            if (includeProps != null)
            {
                foreach (var item in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            return await query.ToListAsync();
        }

        public async Task Remove(T entity)
        {
            dbset.Remove(entity); await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
