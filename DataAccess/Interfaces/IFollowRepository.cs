using System.Linq;
using System.Threading.Tasks;
using OnlyFundsAPI.BusinessObjects;

namespace OnlyFundsAPI.DataAccess.Interfaces
{
    public interface IFollowRepository
    {
        IQueryable<Follow> GetList();
        Task<Follow> GetSingle(int followerID, int followeeID);
        Task<Follow> Create(Follow follow);
        Task Delete(int followerID, int followeeID);
    }
}
