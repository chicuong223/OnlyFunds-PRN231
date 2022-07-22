using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    internal class BookmarkRepository : IBookmarkRepository
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

        public async Task Delete(int UserID, int PostID)
        {
            try
            {
                var bookMark = await context.Bookmarks
                    .SingleOrDefaultAsync(cl => cl.PostID == PostID && cl.UserID == UserID)
                    ?? throw new ArgumentException("Bookmark not found");
                context.Bookmarks.Remove(bookMark);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Bookmark> GetBookmark(int UserID, int PostID)
        {
            Bookmark result = null;
            try
            {
                result = await context.Bookmarks
                    .SingleOrDefaultAsync(cl => cl.PostID == PostID && cl.UserID == UserID);
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
                if (await context.Bookmarks.AnyAsync(e => e.PostID == bookmark.PostID && e.UserID == bookmark.UserID))
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