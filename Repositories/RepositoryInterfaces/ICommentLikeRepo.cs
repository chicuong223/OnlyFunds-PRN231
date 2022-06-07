using OnlyFundsAPI.BusinessObjects;
using System.Linq;

namespace Repositories.RepositoryInterfaces
{
    public interface ICommentLikeRepo
    {
        IQueryable<CommentLike> GetList();
        CommentLike Get(int commentId, int userId);
        void Insert(CommentLike inserted);
        void Update(CommentLike updated);
        void Delete(int commentId, int userId);
    }
}