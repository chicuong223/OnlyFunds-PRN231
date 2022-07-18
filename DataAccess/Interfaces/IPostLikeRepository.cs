using OnlyFundsAPI.BusinessObjects;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IPostLikeRepository
    {
        IQueryable<PostLike> GetList();

        Task<PostLike> GetByID(int likerId, int postId);

        Task<PostLike> Create(PostLike postLike);

        Task Delete(int likerId, int postId);
    }
}