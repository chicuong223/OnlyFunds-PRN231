using OnlyFundsAPI.BusinessObjects;
using System.Linq;

namespace Repositories.RepositoryInterfaces
{
    public interface IBookmarkRepo
    {
        IQueryable<Bookmark> GetList();
        Bookmark Get(int userId, int postId);
        void Insert(Bookmark inserted);
        void Update(Bookmark updated);
        void Delete(int userId, int postId);
    }
}