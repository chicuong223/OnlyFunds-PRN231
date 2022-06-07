using OnlyFundsAPI.BusinessObjects;
using System.Linq;

namespace Repositories.RepositoryInterfaces
{
    public interface IPostLikeRepo
    {
        IQueryable<PostLike> GetList();
        PostLike Get(int userId, int postId);
        void Insert(PostLike inserted);
        void Update(PostLike updated);
        void Delete(int userId, int postId);
    }
}