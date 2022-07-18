using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class PostTagMapRepository : IPostTagMapRepository
    {
        private readonly OnlyFundsDBContext context;

        public PostTagMapRepository(OnlyFundsDBContext context)
        {
            this.context = context;
        }

        public async Task<PostTagMap> Create(PostTagMap postTagMap)
        {
            try
            {
                if (!await context.PostTagMaps.AnyAsync(e => e.TagID == postTagMap.TagID && e.PostID == postTagMap.PostID))
                {
                    await context.PostTagMaps.AddAsync(postTagMap);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Post-Tag Map existed");
                }
            }
            catch
            {
                throw;
            }
            return postTagMap;
        }

        public async Task Delete(int tagId, int postId)
        {
            try
            {
                PostTagMap postTagMap = await context.PostTagMaps.SingleOrDefaultAsync(e => e.TagID == tagId && e.PostID == postId);
                if (postTagMap != null)
                {
                    context.PostTagMaps.Remove(postTagMap);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Post-Tag Map not found");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<PostTagMap> GetByID(int tagId, int postId)
        {
            return await context.PostTagMaps.SingleOrDefaultAsync(e => e.TagID == tagId && e.PostID == postId);
        }

        public IQueryable<PostTagMap> GetList()
        {
            return context.PostTagMaps.AsQueryable();
        }

        public async Task<PostTagMap> Update(PostTagMap postTagMap)
        {
            try
            {
                if (await context.PostTagMaps.AnyAsync(e => e.TagID == postTagMap.TagID && e.PostID == postTagMap.PostID))
                {
                    context.PostTagMaps.Update(postTagMap);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Post-Tag Map not found");
                }
            }
            catch
            {
                throw;
            }
            return postTagMap;
        }
    }
}