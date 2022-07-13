using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;

namespace OnlyFundsAPI.API.Controllers
{
    public class CommentLikesController : ODataController
    {
        private readonly IRepoWrapper repo;
        public CommentLikesController(IRepoWrapper repo)
        {
            this.repo = repo;
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int keyUserId, int keyCommentId)
        {
            var result = await repo.CommentLikes.GetCommentLike(keyUserId, keyCommentId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Post([FromBody] CommentLike commentLike)
        {
            ModelState.ClearValidationState(nameof(CommentLike));
            var comment = await repo.Comments.GetByID(commentLike.CommentID);
            if (comment == null || !comment.IsActive) return BadRequest("Comment not found!");
            var currentUserID = GetCurrentUserID();
            var existingLike = await repo.CommentLikes.GetCommentLike(currentUserID.Value, commentLike.CommentID);
            if (existingLike != null) return BadRequest("User has liked this comment");
            commentLike.UserID = currentUserID.Value;
            var result = await repo.CommentLikes.Create(commentLike);
            return Created(result);
        }

        [Authorize(Roles = "User")]
        [HttpDelete("odata/{keyCommentID}")]
        public async Task<IActionResult> Delete(int keyCommentID)
        {
            var currentUserID = GetCurrentUserID();
            // if (currentUserID.Value != keyUserId) return Forbid();
            var commentLike = await repo.CommentLikes.GetCommentLike(currentUserID.Value, keyCommentID);
            if (commentLike == null) return NotFound();
            await repo.CommentLikes.Delete(currentUserID.Value, keyCommentID);
            return NoContent();
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