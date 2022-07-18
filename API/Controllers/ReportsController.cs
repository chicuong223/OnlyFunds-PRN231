using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ReportsController : ODataController
    {
        private readonly IRepoWrapper repo;

        public ReportsController(IRepoWrapper repo)
        {
            this.repo = repo;
        }

        // GET: api/Reports
        [EnableQuery]
        public ActionResult<IEnumerable<Report>> Get()
        {
            return Ok(repo.Reports.GetList());
        }

        // GET: api/Reports/5
        [EnableQuery]
        public SingleResult<Report> Get(int key)
        {
            var report = repo.Reports.GetList().Where(e => e.ReportID == key);

            return SingleResult.Create(report);
        }

        // PUT: api/Reports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<IActionResult> Patch(int key, Delta<Report> report)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            object deltaId;
            bool foundId = report.TryGetPropertyValue("ReportID", out deltaId);
            if (!foundId) return BadRequest();
            int id;
            bool tryParse = int.TryParse(deltaId.ToString(), out id);
            if (!tryParse) return BadRequest();
            if (key != id)
            {
                return BadRequest();
            }
            try
            {
                var existingReport = await repo.Reports.GetByID(key);
                if (existingReport == null || existingReport.Status != ReportStatus.Unresolved) return BadRequest("Comment not found!");
                var currentUserID = GetCurrentUserID();
                if (existingReport.ReporterID != currentUserID) return Unauthorized("Modifying other users' comments is not allowed!");
                report.Patch(existingReport);
                var result = await repo.Reports.Update(existingReport);
                return Updated(result);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException) return NotFound(ex.Message);
                throw;
            }
        }

        // POST: api/Reports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Report>> Post([FromBody] Report report)
        {
            ModelState.ClearValidationState(nameof(Report));

            try
            {
                var currentUserID = GetCurrentUserID();
                if (currentUserID == null) return Unauthorized();
                if (report.ReporterID != currentUserID.Value) return Forbid();
                report.ReportTime = DateTime.Now;
                if (!TryValidateModel(report)) return BadRequest();
                var result = await repo.Reports.Create(report);
                return Created(result);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException) return BadRequest(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/Reports/5
        public async Task<IActionResult> Delete(int key)
        {
            try
            {
                var report = await repo.Reports.GetByID(key);
                if (report == null || report.Status != ReportStatus.Unresolved) return NotFound();

                //if current user is not admin
                //check if current user is the uploader of the comment
                //if not, return Unauthorized
                if (!GetCurrentUserRole().Equals("Admin"))
                {
                    var currentUserID = GetCurrentUserID();
                    if (currentUserID != report.ReporterID)
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