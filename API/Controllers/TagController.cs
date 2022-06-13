using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;

namespace OnlyFundsAPI.API.Controllers
{
    public class TagController : ODataController
    {
        private readonly IRepoWrapper repo;
        public TagController(IRepoWrapper repo)
        {
            this.repo = repo;
        }

        [EnableQuery]
        public async Task<IEnumerable<PostTag>> Get()
        {
            var result = await repo.Tags.GetList();
            return result;
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var tag = await repo.Tags.GetByID(key);
            if (tag == null) return NotFound();
            return Ok(tag);
        }

        public async Task<IActionResult> Post([FromBody] PostTag tag)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await repo.Tags.Create(tag);
            return Created(result);
        }

        public async Task<IActionResult> Delete(int key)
        {
            try
            {
                await repo.Tags.Delete(key);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException) return NotFound();
                throw new Exception(ex.Message);
            }
            return NoContent();
        }
    }
}