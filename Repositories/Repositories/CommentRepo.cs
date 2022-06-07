using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using Repositories.RepositoryInterfaces;
using System;
using System.Linq;

namespace Repositories.Repositories
{
    internal class CommentRepo : ICommentRepo
    {
        public Comment Get(int id)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Comment entity = context.Comments.SingleOrDefault(e => e.CommentID == id);
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<Comment> GetList()
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                IQueryable<Comment> list = context.Comments.AsQueryable();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Insert(Comment inserted)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Comment existed = context.Comments.SingleOrDefault(e => e.CommentID == inserted.CommentID);
                if (existed == null)
                {
                    context.Comments.Add(inserted);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Update(Comment updated)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Comment existed = context.Comments.AsNoTracking().SingleOrDefault(e => e.CommentID == updated.CommentID);
                if (existed != null)
                {
                    context.Comments.Update(updated);
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
                Comment existed = context.Comments.SingleOrDefault(e => e.CommentID == id);
                if (existed != null)
                {
                    context.Comments.Remove(existed);
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