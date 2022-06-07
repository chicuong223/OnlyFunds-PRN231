using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using Repositories.RepositoryInterfaces;
using System;
using System.Linq;

namespace Repositories.Repositories
{
    internal class PostLikeRepo : IPostLikeRepo
    {
        public PostLike Get(int userId, int postId)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                PostLike entity = context.PostLikes.SingleOrDefault(e => e.UserID == userId && e.PostID == postId);
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<PostLike> GetList()
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                IQueryable<PostLike> list = context.PostLikes.AsQueryable();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Insert(PostLike inserted)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                PostLike existed = context.PostLikes.SingleOrDefault(e => e.UserID == inserted.UserID && e.PostID == inserted.PostID);
                if (existed == null)
                {
                    context.PostLikes.Add(inserted);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Update(PostLike updated)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                PostLike existed = context.PostLikes.AsNoTracking().SingleOrDefault(e => e.UserID == updated.UserID && e.PostID == updated.PostID);
                if (existed != null)
                {
                    context.PostLikes.Update(updated);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Delete(int userId, int postId)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                PostLike existed = context.PostLikes.SingleOrDefault(e => e.UserID == userId && e.PostID == postId);
                if (existed != null)
                {
                    context.PostLikes.Remove(existed);
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