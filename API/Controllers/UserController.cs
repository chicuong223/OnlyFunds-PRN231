using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;
using OnlyFundsAPI.Utilities;

namespace OnlyFundsAPI.API.Controllers
{
    // [ApiController]
    // [Route("api/users")]
    public class UserController : ODataController
    {
        // private readonly IUserRepository userRepository;
        private readonly IRepoWrapper repo;
        public UserController(IRepoWrapper repo)
        {
            // this.userRepository = userRepository;
            this.repo = repo;
        }

        //Get a list of users
        [EnableQuery]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            // return Ok(await userRepository.GetUsers());
            return Ok(await repo.Users.GetUsers());
        }

        //Get User by ID
        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var result = await repo.Users.GetUserByID(key);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        //Create user
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (ModelState.IsValid)
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
            return BadRequest(ModelState);
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
                user.Patch(existingUser);
                await repo.Users.Update(existingUser);
                return Updated(existingUser);
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

    }
}