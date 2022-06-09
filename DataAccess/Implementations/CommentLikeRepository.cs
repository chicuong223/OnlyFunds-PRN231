using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;

namespace OnlyFundsAPI.DataAccess.Implementations
{
    public class CommentLikeRepository : ICommentLikeRepository
    {
        public async Task<CommentLike> Create(CommentLike commentLike)
        {
            try
            {
                using (var context = new OnlyFundsDBContext())
                {
                    await context.CommentLikes.AddAsync(commentLike);
                    await context.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
            return commentLike;
        }

        public async Task Delete(int userID, int commentID)
        {
            try
            {
                using (var context = new OnlyFundsDBContext())
                {
                    var commentLike = await context.CommentLikes
                        .SingleOrDefaultAsync(cl => cl.CommentID == commentID && cl.UserID == userID)
                        ?? throw new ArgumentException("Comment Like not found");
                    context.CommentLikes.Remove(commentLike);
                    await context.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<CommentLike> GetCommentLike(int userID, int commentID)
        {
            CommentLike result = null;
            try
            {
                using (var context = new OnlyFundsDBContext())
                {
                    result = await context.CommentLikes
                        .SingleOrDefaultAsync(cl => cl.CommentID == commentID && cl.UserID == userID);
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}