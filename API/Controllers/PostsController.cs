using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("odata/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IRepoWrapper repo;

        public PostsController(IRepoWrapper repo)
        {
            this.repo = repo;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            var result = repo.Posts.GetList();
            return Ok(result);
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public SingleResult<Post> GetByID(int key)
        {
            var result = repo.Posts.GetList().Where(cmt => cmt.PostID == key);
            return SingleResult.Create(result);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Post post)
        {
            // System.Console.WriteLine(ModelState.IsValid);
            // if (!ModelState.IsValid) return BadRequest(ModelState);
            System.Console.WriteLine(post == null);
            ModelState.ClearValidationState(nameof(Post));
            try
            {
                var currentUserID = GetCurrentUserID();
                if (currentUserID == null) return Unauthorized();
                var currentUser = await repo.Users.GetUserByID(currentUserID.Value);
                if (currentUser.Banned || !currentUser.Active) return Unauthorized("Action not allowed!");
                post.UploaderID = currentUserID.Value;
                post.UploadTime = DateTime.Now;
                if (!TryValidateModel(post)) return BadRequest();
                var newPost = new Post
                {
                    Active = true,
                    AttachmentType = post.AttachmentType,
                    Description = post.Description,
                    FileURL = post.FileURL,
                    Preview = post.Preview,
                    Status = 0,
                    Title = post.Title,
                    UploaderID = post.UploaderID,
                    UploadTime = DateTime.Now
                };
                var result = await repo.Posts.Create(newPost);
                foreach (var map in post.TagMaps)
                {
                    var newMap = new PostTagMap
                    {
                        TagID = map.TagID,
                        PostID = result.PostID
                    };
                    await repo.PostTagMaps.Create(newMap);
                }

                var follows = repo.Follows.GetList().Where(f => f.FolloweeID == currentUserID.Value);
                if (follows.Count() > 0)
                {
                    Notification noti = null;
                    foreach (var follow in follows)
                    {
                        noti = new Notification
                        {
                            IsRead = false,
                            NotificationTime = DateTime.Now,
                            ReceiverID = follow.FollowerID,
                            Content = $"{currentUser.Username} has uploaded a new post!"
                        };
                        await repo.Notifications.Create(noti);
                    }
                }
                return CreatedAtAction(nameof(GetByID), new { key = result.PostID }, result);
            }
            catch
            {
                throw;
            }
        }

        [Authorize(Roles = "User")]
        [HttpPatch("{key}")]
        public async Task<IActionResult> Patch(int key, [FromBody] Delta<Post> post)
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
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException) return NotFound(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{key}")]
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