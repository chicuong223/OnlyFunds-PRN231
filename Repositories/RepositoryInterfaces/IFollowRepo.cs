using OnlyFundsAPI.BusinessObjects;
using System.Linq;

namespace Repositories.RepositoryInterfaces
{
    public interface IFollowRepo
    {
        IQueryable<Follow> GetList();
        Follow Get(int followerId, int followeeId);
        void Insert(Follow inserted);
        void Update(Follow updated);
        void Delete(int followerId, int followeeId);
    }
}