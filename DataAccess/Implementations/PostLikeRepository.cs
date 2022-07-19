using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class PostLikeRepository : IPostLikeRepository
    {
        private readonly OnlyFundsDBContext context;

        public PostLikeRepository(OnlyFundsDBContext context)
        {
            this.context = context;
        }

        public async Task<PostLike> Create(PostLike postLike)
        {
            try
            {
                if (!await context.PostLikes.AnyAsync(e => e.PostID == postLike.PostID && e.UserID == postLike.UserID))
                {
                    await context.PostLikes.AddAsync(postLike);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("User has already liked this post");
                }
            }
            catch
            {
                throw;
            }
            return postLike;
        }

        public async Task Delete(int likerId, int postId)
        {
            try
            {
                PostLike postLike = await context.PostLikes.SingleOrDefaultAsync(e => e.UserID == likerId && e.PostID == postId);
                if (postLike != null)
                {
                    context.PostLikes.Remove(postLike);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Post Like not found");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<PostLike> GetByID(int likerId, int postId)
        {
            PostLike postLike = null;
            try
            {
                postLike = await context.PostLikes.SingleOrDefaultAsync(e => e.UserID == likerId && e.PostID == postId);
            }
            catch
            {
                throw;
            }
            return postLike;
        }

        public IQueryable<PostLike> GetList()
        {
            try
            {
                return context.PostLikes.AsQueryable();
            }
            catch
            {
                throw;
            }
        }
    }
}