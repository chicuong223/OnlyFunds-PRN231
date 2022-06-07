using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using Repositories.RepositoryInterfaces;
using System;
using System.Linq;

namespace Repositories.Repositories
{
    internal class BookmarkRepo : IBookmarkRepo
    {
        public Bookmark Get(int userId, int postId)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Bookmark entity = context.Bookmarks.SingleOrDefault(e => e.UserID == userId && e.PostID == postId);
                return entity;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<Bookmark> GetList()
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                IQueryable<Bookmark> list = context.Bookmarks.AsQueryable();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Insert(Bookmark inserted)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Bookmark existed = context.Bookmarks.SingleOrDefault(e => e.UserID == inserted.UserID && e.PostID == inserted.PostID);
                if(existed == null)
                {
                    context.Bookmarks.Add(inserted);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Update(Bookmark updated)
        {
            try
            {
                using OnlyFundsDBContext context = new OnlyFundsDBContext();
                Bookmark existed = context.Bookmarks.AsNoTracking().SingleOrDefault(e => e.UserID == updated.UserID && e.PostID == updated.PostID);
                if (existed != null)
                {
                    context.Bookmarks.Update(updated);
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
                Bookmark existed = context.Bookmarks.SingleOrDefault(e => e.UserID == userId && e.PostID == postId);
                if (existed != null)
                {
                    context.Bookmarks.Remove(existed);
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