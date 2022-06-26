using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;

namespace API.Controllers
{
    public class PostTagMapsController : ODataController
    {
        private readonly IRepoWrapper repo;

        public PostTagMapsController(IRepoWrapper repo)
        {
            this.repo = repo;
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int keyTagId, int keyPostId)
        {
            var result = await repo.PostTagMaps.GetByID(keyTagId, keyPostId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Post([FromBody] PostTagMap postTagMap)
        {
            ModelState.ClearValidationState(nameof(PostTagMap));
            var post = await repo.Posts.GetByID(postTagMap.PostID);
            if (post == null || post.Status!=PostStatus.Active) return BadRequest("Post not found!");
            var currentUserID = GetCurrentUserID();
            if (post.UploaderID != currentUserID) return Forbid("You are forbiden to add new tag");
            var result = await repo.PostTagMaps.Create(postTagMap);
            return Created(result);
        }

        [Authorize(Roles = "User")]
        [HttpDelete("odata/{postId}")]
        public async Task<IActionResult> Delete(int postId)
        {
            var currentUserID = GetCurrentUserID();
            // if (currentUserID.Value != keyTagId) return Forbid();
            try 
            {
            await repo.PostTagMaps.Delete(currentUserID.Value, postId);
            }
            catch
            {
                var postTagMap = await repo.PostTagMaps.GetByID(currentUserID.Value, postId);
                if (postTagMap == null) return NotFound();
                else throw;
            }
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
