using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFundsAPI.DataAccess.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        private readonly OnlyFundsDBContext context;

        public CommentRepository(OnlyFundsDBContext context)
        {
            this.context = context;
        }

        public async Task<Comment> Create(Comment comment)
        {
            try
            {
                var user = await context.Users.FindAsync(comment.UploaderID);
                var post = await context.Posts.FindAsync(comment.PostID);
                if (user == null || !user.Active || user.Banned) throw new ArgumentException("User not found!");
                if (post == null || !post.Active) throw new ArgumentException("Post not found!");
                // comment.CommentTime = DateTime.Now;
                await context.AddAsync<Comment>(comment);
                await context.SaveChangesAsync();
                return comment;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (DbUpdateConcurrencyException dex)
            {
                throw new DbUpdateConcurrencyException(dex.Message);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                var comment = await context.Comments.FindAsync(id) ?? throw new ArgumentException("Comment not found");
                comment.IsActive = false;
                context.Entry(comment).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Comment> GetByID(int key)
        {
            Comment comment = null;
            try
            {
                comment = await context.Comments.FindAsync(key);
            }
            catch
            {
                throw;
            }
            return comment;
        }

        public IQueryable<Comment> GetList()
        {
            IQueryable<Comment> result;
            try
            {
                result = context.Comments.AsQueryable();
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<Comment> Update(Comment comment)
        {
            try
            {
                Comment commentToUpdate = null;
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                commentToUpdate = await context.Comments.FindAsync(comment.CommentID) ?? throw new ArgumentException("Comment not found");
                commentToUpdate.Content = comment.Content;
                commentToUpdate.CommentTime = DateTime.Now;
                context.Entry(comment).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return commentToUpdate;
            }
            catch
            {
                throw;
            }
        }
    }
}