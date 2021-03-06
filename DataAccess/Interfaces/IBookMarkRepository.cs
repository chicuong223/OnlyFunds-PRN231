using OnlyFundsAPI.BusinessObjects;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IBookmarkRepository
    {
        IQueryable<Bookmark> GetList();

        Task<Bookmark> GetBookmark(int userId, int postId);

        Task<Bookmark> Create(Bookmark bookmark);

        Task<Bookmark> Update(Bookmark bookmark);

        Task Delete(int userId, int postId);
    }
}