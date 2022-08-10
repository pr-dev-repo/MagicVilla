using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repo.IRepo
{
    public interface IVillaNumberRepo : IRepo<VillaNumber>
    {
        Task<VillaNumber> Update(VillaNumber entity);
    }
}
