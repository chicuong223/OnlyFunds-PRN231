using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;
using OnlyFundsAPI.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFundsAPI.API.Controllers
{
    // [ApiController]
    // [Route("api/users")]
    public class UsersController : ODataController
    {
        // private readonly IUserRepository userRepository;
        private readonly IRepoWrapper repo;

        private OnlyFundsDBContext context;

        public UsersController(IRepoWrapper repo, OnlyFundsDBContext context)
        {
            // this.userRepository = userRepository;
            this.repo = repo;
            this.context = context;
        }


        //Get a list of users
        [EnableQuery]
        [Authorize]
        public IActionResult Get()
        {
            // return Ok(await userRepository.GetUsers());
            return Ok(repo.Users.GetUsers());
        }

        //Get User by ID
        [EnableQuery]
        public SingleResult<User> Get(int key)
        {
            var result = repo.Users.GetUsers().Where(user => user.UserID == key);
            return SingleResult.Create(result);
        }

        //Create user
        public async Task<IActionResult> Post([FromBody] User user)
        {
            //if (ModelState.IsValid)
            {
                try
                {
                    user.Password = PasswordUtils.HashString(user.Password);
                    var createdUser = await repo.Users.Create(user);
                    return Created(user);
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentException)
                    {
                        return BadRequest(ex.Message);
                    }
                    throw;
                }
            }
        }

        //update user
        public async Task<IActionResult> Patch(int key, [FromBody] Delta<User> user)
        {
            try
            {
                var existingUser = await repo.Users.GetUserByID(key);
                if (existingUser == null)
                {
                    return NotFound();
                }
                object pass = "";
                var value = user.TryGetPropertyValue("Password", out pass);
                if (pass != null && !string.IsNullOrEmpty(pass.ToString().Trim()))
                {
                    user.TrySetPropertyValue("Password", PasswordUtils.HashString(pass.ToString()));
                    user.Patch(existingUser);
                }
                else
                {
                    Delta<User> newDelta = new Delta<User>();
                    if (user.TryGetPropertyValue("Username", out object username))
                    {
                        if (username != null && !string.IsNullOrEmpty(username.ToString()))
                            newDelta.TrySetPropertyValue("Username", username);
                    }
                    if (user.TryGetPropertyValue("Email", out object email))
                    {
                        if (email != null && !string.IsNullOrEmpty(email.ToString()))
                            newDelta.TrySetPropertyValue("Email", email);
                    }
                    if (user.TryGetPropertyValue("FirstName", out object firstName))
                    {
                        if (firstName != null && !string.IsNullOrEmpty(firstName.ToString()))
                            newDelta.TrySetPropertyValue("FirstName", firstName);
                    }
                    if (user.TryGetPropertyValue("LastName", out object lastName))
                    {
                        if (lastName != null && !string.IsNullOrEmpty(lastName.ToString()))
                            newDelta.TrySetPropertyValue("LastName", lastName);
                    }

                    if (user.TryGetPropertyValue("AvatarUrl", out object avatarUrl))
                    {
                        if (avatarUrl != null && !string.IsNullOrEmpty(avatarUrl.ToString()))
                            newDelta.TrySetPropertyValue("AvatarUrl", avatarUrl);
                    }
                    newDelta.Patch(existingUser);
                }
                await context.SaveChangesAsync();
                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    return BadRequest(ex.Message);
                }
                throw;
            }
        }

        //Ban a user
        public async Task<IActionResult> Put(int key)
        {
            var user = await repo.Users.GetUserByID(key);
            if (user == null || !user.Active) return NotFound();
            user.Banned = true;
            await repo.Users.Update(user);
            return Updated(user);
        }

        //Deactive a user
        public async Task<IActionResult> Delete(int key)
        {
            // var user = await repo.Users.GetUserByID(key);
            // if (user == null || !user.Active) return NotFound();
            // await repo.Users.Delete(key);
            // return NoContent();
            try
            {
                await repo.Users.Delete(key);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException) return NotFound();
                throw new Exception(ex.Message);
            }
        }

        //Change Password
        [HttpPost("odata/[controller]/change-password")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ChangePassword(string newPassword, string confirmPassword)
        {
            var userID = GetCurrentUserID().Value;
            var user = await repo.Users.GetUserByID(userID);
            if (user == null) return BadRequest("User not found!");
            if (!user.Active || user.Banned) return Forbid("Your account is not active!");
            string hashedPassword = Utilities.PasswordUtils.HashString(newPassword);
            string hashedConfirmPassword = Utilities.PasswordUtils.HashString(confirmPassword);
            if (!hashedPassword.Equals(hashedConfirmPassword)) return BadRequest("Confirm password does not match!");
            user.Password = hashedPassword;
            await repo.Users.Update(user);
            return Updated(user);
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