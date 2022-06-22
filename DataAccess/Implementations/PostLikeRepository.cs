using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    throw new ArgumentException("Notification existed");
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
            try
            {
                PostLike postLike = await context.PostLikes.SingleOrDefaultAsync(e => e.UserID == likerId && e.PostID == postId);
                if (postLike != null)
                {
                    return postLike;
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
