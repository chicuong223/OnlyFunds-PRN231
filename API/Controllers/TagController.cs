using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
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
        public IActionResult Get()
        {
            var result = repo.Tags.GetList();
            return Ok(result);
        }

        [EnableQuery]
        public SingleResult<PostTag> Get(int key)
        {
            // var tag = await repo.Tags.GetByID(key);
            // if (tag == null) return NotFound();
            // return Ok(tag);
            var result = repo.Tags.GetList().Where(tag => tag.TagID == key);
            return SingleResult.Create(result);
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

        public async Task<IActionResult> Patch(int key, [FromBody] Delta<PostTag> tag)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = await repo.Tags.GetByID(key);
            if (entity == null) return NotFound();
            tag.Patch(entity);
            await repo.Tags.Update(entity);
            return Updated(entity);
        }
    }
}