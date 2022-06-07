using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using Repositories.RepositoryInterfaces;
using System;
using System.Linq;

namespace Repositories.Repositories
{
    internal class PostRepo : IPostRepo
    {
        public Post Get(int id)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Post entity = context.Posts.SingleOrDefault(e => e.PostID == id);
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<Post> GetList()
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                IQueryable<Post> list = context.Posts.AsQueryable();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Insert(Post inserted)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Post existed = context.Posts.SingleOrDefault(e => e.PostID == inserted.PostID);
                if (existed == null)
                {
                    context.Posts.Add(inserted);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Update(Post updated)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Post existed = context.Posts.AsNoTracking().SingleOrDefault(e => e.PostID == updated.PostID);
                if (existed != null)
                {
                    context.Posts.Update(updated);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Delete(int id)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Post existed = context.Posts.SingleOrDefault(e => e.PostID == id);
                if (existed != null)
                {
                    context.Posts.Remove(existed);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}