using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;

namespace OnlyFundsAPI.API.Controllers
{
    public class CommentController : ODataController
    {
        private readonly IRepoWrapper repo;
        public CommentController(IRepoWrapper repo)
        {
            this.repo = repo;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var result = (await repo.Comments.GetList()).AsQueryable();
            return Ok(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var result = await repo.Comments.GetByID(key);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Post([FromBody] Comment comment)
        {
            // System.Console.WriteLine(ModelState.IsValid);
            // if (!ModelState.IsValid) return BadRequest(ModelState);
            ModelState.ClearValidationState(nameof(Comment));
            try
            {
                var currentUserID = GetCurrentUserID();
                if (currentUserID == null) return Unauthorized();
                comment.UploaderID = currentUserID.Value;
                comment.CommentTime = DateTime.Now;
                if (!TryValidateModel(comment)) return BadRequest();
                var result = await repo.Comments.Create(comment);
                return Created(result);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException) return BadRequest(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Patch(int key, Delta<Comment> comment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var existingCmt = await repo.Comments.GetByID(key);
                if (existingCmt == null || !existingCmt.IsActive) return BadRequest("Comment not found!");
                var currentUserID = GetCurrentUserID();
                if (existingCmt.UploaderID != currentUserID) return Unauthorized("Modifying other users' comments is not allowed!");
                comment.Patch(existingCmt);
                var result = await repo.Comments.Update(existingCmt);
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
                var comment = await repo.Comments.GetByID(key);
                if (comment == null || !comment.IsActive) return NotFound();

                //if current user is not admin
                //check if current user is the uploader of the comment
                //if not, return Unauthorized
                if (!GetCurrentUserRole().Equals("Admin"))
                {
                    var currentUserID = GetCurrentUserID();
                    if (currentUserID != comment.UploaderID)
                    {
                        return Unauthorized("Deleting other users' comments is not allowed!");
                    }
                }
                await repo.Comments.Delete(key);
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