using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;

namespace OnlyFundsAPI.API.Controllers
{
    public class FollowsController : ODataController
    {
        private readonly IRepoWrapper repo;
        public FollowsController(IRepoWrapper repo)
        {
            this.repo = repo;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            var result = repo.Follows.GetList();
            return Ok(result);
        }

        [EnableQuery]
        public SingleResult<Follow> Get(int keyFollowerID, int keyFolloweeID)
        {
            var result = repo.Follows.GetList().Where(follow => follow.FolloweeID == keyFolloweeID && follow.FollowerID == keyFollowerID);
            return SingleResult.Create(result);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Post([FromBody] Follow follow)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await repo.Follows.Create(follow);
                return Created(result);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException || ex is ArgumentOutOfRangeException)
                {
                    return BadRequest(ex.Message);
                }
                throw;
            }
        }

        [Authorize]
        public async Task<IActionResult> Delete(int keyFolloweeID, int keyFollowerID)
        {
            try
            {
                await repo.Follows.Delete(keyFollowerID, keyFolloweeID);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException) return NotFound();
                throw;
            }
            return NoContent();
        }
    }
}