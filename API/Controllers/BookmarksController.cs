using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;

namespace API.Controllers
{
    public class BookmarksController : ODataController
    {
        private readonly IRepoWrapper _repo;

        public BookmarksController(IRepoWrapper repo)
        {
            _repo = repo;
        }

        // GET: api/Bookmarks
        [EnableQuery]
        public ActionResult<IEnumerable<Bookmark>> Get()
        {
            return Ok(_repo.Bookmarks.GetList());
        }

        // GET: api/Bookmarks/5
        [EnableQuery]
        public async Task<SingleResult<Bookmark>> Get(int keyUserId, int keyPostId)
        {
            var result = _repo.Bookmarks.GetList().Where(e => e.UserID == keyUserId && e.PostID == keyPostId);

            return SingleResult.Create(result);
        }

        // PUT: api/Bookmarks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<IActionResult> Patch(int keyUserId, int keyPostId, Delta<Bookmark> bookmark)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var existingBookmark = await _repo.Bookmarks.GetBookmark(keyUserId, keyPostId);
                if (existingBookmark == null) return BadRequest("Bookmark not found!");
                var currentUserID = GetCurrentUserID();
                if (existingBookmark.UserID != currentUserID) return Unauthorized("Bookmarking for others is not allowed!");
                bookmark.Patch(existingBookmark);
                var result = await _repo.Bookmarks.Update(existingBookmark);
                return Updated(result);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException) return NotFound(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        // POST: api/Bookmarks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Bookmark>> Post(Bookmark bookmark)
        {
            ModelState.ClearValidationState(nameof(Bookmark));
            var post = await _repo.Posts.GetByID(bookmark.PostID);
            if (post == null || post.Status != PostStatus.Active)
                return BadRequest("Post not found!");
            var currentUserID = GetCurrentUserID();
            if (BookmarkExists(bookmark.UserID, bookmark.PostID))
                return BadRequest("User has liked this comment");
            bookmark.UserID = currentUserID.Value;
            var result = await _repo.Bookmarks.Create(bookmark);
            return Created(result);
        }

        // DELETE: api/Bookmarks/5
        [Authorize(Roles = "User")]
        [HttpDelete("odata/{postId}")]
        public async Task<IActionResult> DeleteBookmark(int postId)
        {
            var currentUserID = GetCurrentUserID();
            // if (currentUserID.Value != keyUserId) return Forbid();
            try
            {
                await _repo.CommentLikes.Delete(currentUserID.Value, postId);
            }
            catch 
            {
                var bokkmark = await _repo.Bookmarks.GetBookmark(currentUserID.Value, postId);
                if (bokkmark == null) return NotFound();
                throw;
            }
            return NoContent();
        }

        private bool BookmarkExists(int keyUserId, int keyPostId)
        {
            return _repo.Bookmarks.GetList().SingleOrDefault(e => e.UserID == keyUserId && e.PostID == keyPostId) != null; ;
        }
        private int? GetCurrentUserID()
        {
            var result = this.User ?? throw new UnauthorizedAccessException("Not logged in!");
            var idStr = result.FindFirst("UserId");
            if (idStr != null) return Int32.Parse(idStr.Value);
            return null;
        }
    }
}
