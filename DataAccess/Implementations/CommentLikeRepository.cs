using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnlyFundsAPI.DataAccess.Implementations
{
    public class CommentLikeRepository : ICommentLikeRepository
    {
        private readonly OnlyFundsDBContext context;

        public CommentLikeRepository(OnlyFundsDBContext context)
        {
            this.context = context;
        }

        public async Task<CommentLike> Create(CommentLike commentLike)
        {
            try
            {
                await context.CommentLikes.AddAsync(commentLike);
                await context.SaveChangesAsync();
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
                var commentLike = await context.CommentLikes
                    .SingleOrDefaultAsync(cl => cl.CommentID == commentID && cl.UserID == userID)
                    ?? throw new ArgumentException("Comment Like not found");
                context.CommentLikes.Remove(commentLike);
                await context.SaveChangesAsync();
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
                result = await context.CommentLikes
                    .SingleOrDefaultAsync(cl => cl.CommentID == commentID && cl.UserID == userID);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}