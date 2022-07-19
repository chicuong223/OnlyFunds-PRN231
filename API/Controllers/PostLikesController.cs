using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class PostLikesController : ODataController
    {
        private readonly IRepoWrapper repo;

        public PostLikesController(IRepoWrapper repo)
        {
            this.repo = repo;
        }


        [EnableQuery]
        public IActionResult GetPostLikes()
        {
            var result = repo.PostLikes.GetList();
            return Ok(result);
        }

        [EnableQuery]
        public SingleResult<PostLike> Get(int keyUserID, int keyPostID)
        {
            var result = repo.PostLikes.GetList().Where(like => like.UserID == keyUserID && like.PostID == keyPostID);
            return SingleResult.Create(result);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Post([FromBody] PostLike postLike)
        {
            ModelState.ClearValidationState(nameof(PostLike));
            var post = await repo.Posts.GetByID(postLike.PostID);
            if (post == null || post.Status != PostStatus.Active) return BadRequest("Post not found!");
            var currentUserID = GetCurrentUserID();
            var existingLike = await repo.PostLikes.GetByID(currentUserID.Value, postLike.PostID);
            if (existingLike != null) return BadRequest("User has liked this post");
            postLike.UserID = currentUserID.Value;
            var result = await repo.PostLikes.Create(postLike);
            return Created(result);
        }

        [Authorize(Roles = "User")]
        [HttpDelete("odata/[controller]/{keyPostID}")]
        public async Task<IActionResult> Delete(int keyPostID)
        {
            var currentUserID = GetCurrentUserID();
            // if (currentUserID.Value != keyUserId) return Forbid();
            var postLike = await repo.PostLikes.GetByID(currentUserID.Value, keyPostID);
            if (postLike == null) return NotFound();
            await repo.PostLikes.Delete(currentUserID.Value, keyPostID);
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