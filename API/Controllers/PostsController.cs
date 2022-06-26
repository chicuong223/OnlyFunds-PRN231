using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class PostsController : ODataController
    {
        private readonly IRepoWrapper repo;

        public PostsController(IRepoWrapper repo)
        {
            this.repo = repo;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            var result = repo.Posts.GetList();
            return Ok(result);
        }

        [EnableQuery]
        public SingleResult<Post> Get(int key)
        {
            var result = repo.Posts.GetList().Where(cmt => cmt.PostID == key);
            return SingleResult.Create(result);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            // System.Console.WriteLine(ModelState.IsValid);
            // if (!ModelState.IsValid) return BadRequest(ModelState);
            ModelState.ClearValidationState(nameof(Post));
            try
            {
                var currentUserID = GetCurrentUserID();
                if (currentUserID == null) return Unauthorized();
                post.UploaderID = currentUserID.Value;
                post.UploadTime = DateTime.Now;
                if (!TryValidateModel(post)) return BadRequest();
                var result = await repo.Posts.Create(post);
                return Created(result);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException) return BadRequest(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Patch(int key, Delta<Post> post)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var existingPost = await repo.Posts.GetByID(key);
                if (existingPost == null || existingPost.Status != PostStatus.Active) return BadRequest("Post not found!");
                var currentUserID = GetCurrentUserID();
                if (existingPost.UploaderID != currentUserID) return Unauthorized("Modifying other users' posts is not allowed!");
                post.Patch(existingPost);
                var result = await repo.Posts.Update(existingPost);
                return Updated(result);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException) return NotFound(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> Delete(int key)
        {
            try
            {
                var post = await repo.Posts.GetByID(key);
                if (post == null || post.Status != PostStatus.Active) return NotFound();

                //if current user is not admin
                //check if current user is the uploader of the post
                //if not, return Unauthorized
                if (!GetCurrentUserRole().Equals("Admin"))
                {
                    var currentUserID = GetCurrentUserID();
                    if (currentUserID != post.UploaderID)
                    {
                        return Unauthorized("Deleting other users' posts is not allowed!");
                    }
                }
                await repo.Posts.Delete(key);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException) return NotFound(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private int? GetCurrentUserID()
        {
            var result = this.User ?? throw new UnauthorizedAccessException("Not logged in!");
            var idStr = result.FindFirst("UserId");
            if (idStr != null) return Int32.Parse(idStr.Value);
            return null;
        }

        private string GetCurrentUserRole()
        {
            var result = this.User ?? throw new UnauthorizedAccessException("Not logged in!");
            var role = result.FindFirst(ClaimTypes.Role);
            if (role != null) return role.Value;
            return null;
        }
    }
}
