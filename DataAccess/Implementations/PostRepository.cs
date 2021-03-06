using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class PostRepository : IPostRepository
    {
        private readonly OnlyFundsDBContext context;

        public PostRepository(OnlyFundsDBContext context)
        {
            this.context = context;
        }

        public async Task<Post> Create(Post post)
        {
            try
            {
                // if (!await context.Posts.AnyAsync(e => e.PostID == post.PostID))
                // {
                await context.Posts.AddAsync(post);
                await context.SaveChangesAsync();
                return post;
                // }
                // else
                // {
                //     throw new ArgumentException("Post existed");
                // }
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                Post post = await context.Posts.SingleOrDefaultAsync(e => e.PostID == id);
                if (post != null)
                {
                    post.Active = !post.Active;
                    context.Posts.Update(post);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Post not found");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<Post> GetByID(int id)
        {
            try
            {
                Post post = await context.Posts.Include(post => post.Uploader).SingleOrDefaultAsync(e => e.PostID == id);
                return post;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<Post> GetList()
        {
            IQueryable<Post> result;
            try
            {
                result = context.Posts.AsQueryable();
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<Post> Update(Post post)
        {
            try
            {
                if (await context.Posts.AnyAsync(e => e.PostID == post.PostID))
                {
                    context.Posts.Update(post);
                    await context.SaveChangesAsync();
                    return post;
                }
                else
                {
                    throw new ArgumentException("Post not found");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}