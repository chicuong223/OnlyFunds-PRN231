using System.Threading.Tasks;
using OnlyFundsAPI.BusinessObjects;

namespace OnlyFundsAPI.DataAccess.Interfaces
{
    public interface ICommentLikeRepository
    {
        Task<CommentLike> GetCommentLike(int userID, int commentID);
        Task Delete(int userID, int commentID);
        Task<CommentLike> Create(CommentLike commentLike);
    }
}