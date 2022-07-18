using OnlyFundsAPI.BusinessObjects;
using System.Threading.Tasks;

namespace OnlyFundsAPI.DataAccess.Interfaces
{
    public interface ICommentLikeRepository
    {
        Task<CommentLike> GetCommentLike(int userID, int commentID);

        Task Delete(int userID, int commentID);

        Task<CommentLike> Create(CommentLike commentLike);
    }
}