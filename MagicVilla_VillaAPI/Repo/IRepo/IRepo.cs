using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repo.IRepo
{
    public interface IRepo<T> where T: class
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProps = null, int pageSize = 0, int pageNumber = 1); // default is 3
        Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProps = null);
        Task Create(T entity);
        Task Remove(T entity);
        Task Save();
    }
}
