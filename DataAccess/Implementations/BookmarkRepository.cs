using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    class BookmarkRepository : IBookmarkRepository
    {
        private readonly OnlyFundsDBContext context;
        public BookmarkRepository(OnlyFundsDBContext context)
        {
            this.context = context;
        }
        public async Task<Bookmark> Create(Bookmark bookMark)
        {
            try
            {
                await context.Bookmarks.AddAsync(bookMark);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return bookMark;
        }

        public async Task Delete(int userID, int commentID)
        {
            try
            {
                var bookMark = await context.Bookmarks
                    .SingleOrDefaultAsync(cl => cl.UserID == userID && cl.UserID == userID)
                    ?? throw new ArgumentException("Bookmark not found");
                context.Bookmarks.Remove(bookMark);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Bookmark> GetBookmark(int userID, int commentID)
        {
            Bookmark result = null;
            try
            {
                result = await context.Bookmarks
                    .SingleOrDefaultAsync(cl => cl.UserID == userID && cl.UserID == userID);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public IQueryable<Bookmark> GetList()
        {
            IQueryable<Bookmark> result;
            try
            {
                result = context.Bookmarks.AsQueryable();
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<Bookmark> Update(Bookmark bookmark)
        {
            try
            {
                if (await context.Bookmarks.AnyAsync(e=>e.PostID==bookmark.PostID&&e.UserID==bookmark.UserID))
                {
                    context.Bookmarks.Update(bookmark);
                    await context.SaveChangesAsync();
                    return bookmark;
                }
                else
                {
                    throw new ArgumentException("Bookmark not found");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
