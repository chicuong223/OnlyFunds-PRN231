using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;

namespace OnlyFundsAPI.DataAccess.Implementations
{
    public class TagRepository : ITagRepository
    {
        public async Task<PostTag> Create(PostTag tag)
        {
            PostTag result = null;
            try
            {
                using (var context = new OnlyFundsDBContext())
                {
                    var existingName = await context.PostTags
                        .FirstOrDefaultAsync(t => t.TagName.ToLower().Equals(tag.TagName.ToLower()));

                    //check if a tag with input name exists
                    //if tag exists: 
                    //check if it is active
                    //if not, change to active
                    //else, throw ArgumentException
                    if (existingName != null)
                    {
                        if (existingName.Active) throw new ArgumentException("Tag with this name exists!");
                        else
                        {
                            existingName.Active = true;
                            context.Entry(existingName).State = EntityState.Modified;
                            result = existingName;
                        }
                    }
                    else
                    {
                        await context.PostTags.AddAsync(tag);
                        result = tag;
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task Delete(int id)
        {
            try
            {
                using (var context = new OnlyFundsDBContext())
                {
                    context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                    var tag = await context.PostTags
                        .SingleOrDefaultAsync(t => t.TagID == id && t.Active)
                        ?? throw new ArgumentException("Tag not found!");
                    tag.Active = false;
                    context.PostTags.Update(tag);
                    await context.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<PostTag> GetByID(int id)
        {
            PostTag result = null;
            try
            {
                using (var context = new OnlyFundsDBContext())
                {
                    result = await context.PostTags.FindAsync(id);
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<IEnumerable<PostTag>> GetList()
        {
            IEnumerable<PostTag> result = new List<PostTag>();
            try
            {
                using (var context = new OnlyFundsDBContext())
                {
                    result = await context.PostTags.ToListAsync();
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public Task<PostTag> Update(PostTag tag)
        {
            throw new System.NotImplementedException();
        }
    }
}