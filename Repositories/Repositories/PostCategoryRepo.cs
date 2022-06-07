using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using Repositories.RepositoryInterfaces;
using System;
using System.Linq;

namespace Repositories.Repositories
{
    internal class PostCategoryRepo : IPostCategoryRepo
    {
        public PostCategory Get(int id)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                PostCategory entity = context.PostCategories.SingleOrDefault(e => e.CategoryID == id);
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<PostCategory> GetList()
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                IQueryable<PostCategory> list = context.PostCategories.AsQueryable();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Insert(PostCategory inserted)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                PostCategory existed = context.PostCategories.SingleOrDefault(e => e.CategoryID == inserted.CategoryID);
                if (existed == null)
                {
                    context.PostCategories.Add(inserted);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Update(PostCategory updated)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                PostCategory existed = context.PostCategories.AsNoTracking().SingleOrDefault(e => e.CategoryID == updated.CategoryID);
                if (existed != null)
                {
                    context.PostCategories.Update(updated);
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
                PostCategory existed = context.PostCategories.SingleOrDefault(e => e.CategoryID == id);
                if (existed != null)
                {
                    context.PostCategories.Remove(existed);
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