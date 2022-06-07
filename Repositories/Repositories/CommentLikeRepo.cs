using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using Repositories.RepositoryInterfaces;
using System;
using System.Linq;

namespace Repositories.Repositories
{
    internal class CommentLikeRepo : ICommentLikeRepo
    {
        public CommentLike Get(int commentId, int userId)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                CommentLike entity = context.CommentLikes.SingleOrDefault(e => e.CommentID == commentId && e.UserID == userId);
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<CommentLike> GetList()
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                IQueryable<CommentLike> list = context.CommentLikes.AsQueryable();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Insert(CommentLike inserted)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                CommentLike existed = context.CommentLikes.SingleOrDefault(e => e.CommentID == inserted.CommentID && e.UserID == inserted.UserID);
                if (existed == null)
                {
                    context.CommentLikes.Add(inserted);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Update(CommentLike updated)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                CommentLike existed = context.CommentLikes.AsNoTracking().SingleOrDefault(e => e.CommentID == updated.CommentID && e.UserID == updated.UserID);
                if (existed != null)
                {
                    context.CommentLikes.Update(updated);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Delete(int commentId, int userId)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                CommentLike existed = context.CommentLikes.SingleOrDefault(e => e.CommentID == commentId && e.UserID == userId);
                if (existed != null)
                {
                    context.CommentLikes.Remove(existed);
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