using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repo.IRepo
{
    public interface IVillaRepo : IRepo<Villa>
    {
        Task<Villa> Update(Villa entity);
    }
}
